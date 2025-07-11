using AudioGet;
using AW_Lib;
using Detect;
using NAudio.Wave;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Musik
{
    internal class MusikConsole
    {



        public static void konstrukt()
        {
            Start("Musik");
            Path();
            MusikHauptmenu();
        }



        static void Start(string title)
        {
            // new APP Info Interface
            IAppInfo appInfo = new AppInfo();

            appInfo.Title = "Musik";
            appInfo.Version = "0.1";
            appInfo.Title = title;


            // Header with AWET
            var header = new FigletText(appInfo.Title)
                .Centered()
                .Color(Color.Blue);
            // APP Version separator
            var versionSeparator = new Rule($"[red]{appInfo.Version}[/]")
                .Centered();
            // Date
            appInfo.currentDate = DateTime.Now;
            // Render the interface
            AnsiConsole.Write(header);
            AnsiConsole.Write(versionSeparator);


        }

        // Musik Lokal und URL Return von path

        static void Path()
        {
            var pfad = "";
            Upload Upload = new Upload();
            // Create a menu
            var menu = new SelectionPrompt<string>()
                .Title("[red]Menü:[/]")
                .PageSize(3)
                .AddChoices(new[] { "Local Path", "URL", });

            var selectedOption = AnsiConsole.Prompt(menu);

            switch(selectedOption)
            {
                case "Local Path":
                    pfad = AnsiConsole.Ask<string>("Kompletter [green]Pfad[/]!");
                    
                    pfad = Upload.Path;
                    break;
                case "URL":
                    var URL = AnsiConsole.Ask<string>("Komplette [red]URL[/]");

                    URL = Upload.Path;
                    break;

                default:
                    Console.Clear();
                    Path();
                    break;
            }
        
        }
        static void MusikHauptmenu()
        {
            IAppInfo appInfo = new AppInfo();

            // Create a menu
            var menu = new SelectionPrompt<string>()
                .Title("[red]Menü:[/]")
                .PageSize(3)
                .AddChoices(new[] { "Analyzer", "Downloader", "Converter" });


            var selectedOption = AnsiConsole.Prompt(menu);

            // Output the selected option
            AnsiConsole.MarkupLine($"[red]Starte:[/] {selectedOption}");

            switch (selectedOption)
            {
                // AN
                case "Analyzer":
                    Console.Clear();
                    Start("Analyzer");
                    Analyzer();
                    break;
                case "Downloader":
                    Console.Clear();
                    Start("Donwloader");
                    break;
                case "Converter":
                    Console.Clear();
                    Start("Converter");
                    break;
                default:
                    Console.Clear();
                    MusikHauptmenu();
                    break;
            }
        }

        static void Analyzer()
        {
            AudioGet.Upload.File();
            BPM bpm = new BPM();
            BPM.bpm();
            Console.ReadKey();
        }
    }

namespace Detect
    {
        public class BPM
        {
            public static (int bpm, string key) AnalyzeTrack()
            {
                Upload upload = new Upload();
                string audioFilePath = Upload.Path;

                using (var audioFile = new AudioFileReader(audioFilePath))
                {
                    const int windowSize = 2048; // Verbesserte FFT-Fenstergröße
                    var sampleRate = audioFile.WaveFormat.SampleRate;

                    var bpm = AnalyzeBpm(audioFile, windowSize, sampleRate);
                    var key = DetectKey(audioFile, windowSize, sampleRate);

                    Console.WriteLine($"BPM: {bpm}");
                    Console.WriteLine($"Tonart: {key}");

                    return (bpm, key);
                }
            }

            private static int AnalyzeBpm(AudioFileReader audioFile, int windowSize, int sampleRate)
            {
                var samples = new List<float>();
                var buffer = new float[windowSize];

                while (audioFile.Position < audioFile.Length)
                {
                    int samplesRead = audioFile.Read(buffer, 0, windowSize);
                    if (samplesRead == 0) break;

                    // RMS-Energie berechnen
                    float rms = (float)Math.Sqrt(buffer.Take(samplesRead).Select(x => x * x).Average());
                    samples.Add(rms);
                }

                // Tempo-Analyse durch Autokorrelation
                var tempo = DetectTempo(samples.ToArray(), sampleRate / windowSize);
                return (int)tempo;
            }

            private static float DetectTempo(float[] samples, float samplesPerSecond)
            {
                var correlation = new float[samples.Length];

                // Autokorrelation berechnen
                for (int lag = 0; lag < samples.Length / 2; lag++)
                {
                    float sum = 0;
                    for (int i = 0; i < samples.Length - lag; i++)
                    {
                        sum += samples[i] * samples[i + lag];
                    }
                    correlation[lag] = sum;
                }

                // Peaks finden im BPM-Bereich von 60-180
                var minLag = (int)(samplesPerSecond * 60 / 180);
                var maxLag = (int)(samplesPerSecond * 60 / 60);

                var peakLag = minLag;
                var peakValue = correlation[minLag];

                for (int lag = minLag; lag < maxLag; lag++)
                {
                    if (correlation[lag] > peakValue)
                    {
                        peakValue = correlation[lag];
                        peakLag = lag;
                    }
                }

                return 60 * samplesPerSecond / peakLag;
            }

            private static string DetectKey(AudioFileReader audioFile, int windowSize, int sampleRate)
            {
                // Chromagram für Tonart-Erkennung
                var noteNames = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
                var chromagram = new double[12];

                audioFile.Position = 0;
                var buffer = new float[windowSize];
                var fftBuffer = new Complex[windowSize];

                while (audioFile.Position < audioFile.Length)
                {
                    int samplesRead = audioFile.Read(buffer, 0, windowSize);
                    if (samplesRead == 0) break;

                    // FFT vorbereiten
                    for (int i = 0; i < windowSize; i++)
                    {
                        fftBuffer[i] = i < samplesRead ? new Complex(buffer[i], 0) : Complex.Zero;
                    }

                    // FFT durchführen
                    FFT(fftBuffer);

                    // Chromagram aktualisieren
                    UpdateChromagram(fftBuffer, chromagram, sampleRate, windowSize);
                }

                // Dominante Tonart bestimmen
                int keyIndex = Array.IndexOf(chromagram, chromagram.Max());
                return noteNames[keyIndex];
            }

            private static void FFT(Complex[] buffer)
            {
                int n = buffer.Length;
                if (n <= 1) return;

                var even = new Complex[n / 2];
                var odd = new Complex[n / 2];

                for (int i = 0; i < n / 2; i++)
                {
                    even[i] = buffer[2 * i];
                    odd[i] = buffer[2 * i + 1];
                }

                FFT(even);
                FFT(odd);

                for (int k = 0; k < n / 2; k++)
                {
                    var t = odd[k] * Complex.FromPolarCoordinates(1, -2 * Math.PI * k / n);
                    buffer[k] = even[k] + t;
                    buffer[k + n / 2] = even[k] - t;
                }
            }

            private static void UpdateChromagram(Complex[] fftBuffer, double[] chromagram, int sampleRate, int windowSize)
            {
                for (int i = 0; i < windowSize / 2; i++)
                {
                    double frequency = i * sampleRate / (double)windowSize;
                    if (frequency < 20 || frequency > 2000) continue;

                    int noteIndex = (int)(12 * (Math.Log2(frequency / 440.0) + 4.75)) % 12;
                    chromagram[noteIndex] += fftBuffer[i].Magnitude;
                }
            }
        }
    }
}

