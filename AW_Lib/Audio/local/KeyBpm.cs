using NAudio.Wave;
using System;

namespace AW_Lib.Audio.local
{
    public class BPM
    {
        public static void bpm()
        {
            Upload upload = new Upload();
            string audioFilePath = Upload.Path; // Passe den Pfad zur Audiodatei entsprechend deiner Dateistruktur an

            // Lade die Audiodatei mit NAudio
            using (var audioFile = new AudioFileReader(audioFilePath))
            {
                // Konstanten für die BPM-Analyse
                const int windowSize = 1024; // Fenstergröße für FFT
                const int sampleRate = 48000; // Beispiel-Samplerate (anpassen entsprechend der Audiodatei)

                // Analysiere die Audiodaten für BPM
                int bpm = AnalyzeBpm(audioFile, windowSize, sampleRate);

                // Ergebnis ausgeben
                Console.WriteLine($"BPM: {bpm}");

                // Schließe die Audiodatei
                audioFile.Close();
            }
        }

        static int AnalyzeBpm(AudioFileReader audioFile, int windowSize, int sampleRate)
        {
            // Anzahl der Peaks im Zeitbereich
            int peakCount = 0;

            // Anzahl der Abtastpunkte pro Fenster
            int samplesPerWindow = windowSize * audioFile.WaveFormat.Channels;

            // Puffer für die Audiodaten
            float[] buffer = new float[windowSize];

            // Fenster über die Audiodaten verschieben und Peaks zählen
            while (audioFile.Position < audioFile.Length)
            {
                // Lese Audiodaten in den Puffer
                int samplesRead = audioFile.Read(buffer, 0, windowSize);

                // Analyse des Puffers, um Peaks zu zählen
                for (int i = 0; i < samplesRead; i++)
                {
                    // Annahme: Ein Peak tritt auf, wenn der Wert größer als 0 ist
                    if (buffer[i] > 0)
                    {
                        peakCount++;
                    }
                }
            }

            // Berechnung der BPM basierend auf der Anzahl der Peaks
            double seconds = audioFile.Length / (double)sampleRate;
            double beats = peakCount / (double)samplesPerWindow;
            double bpm = beats / seconds * 60;

            return (int)bpm;
        }
    }
}

