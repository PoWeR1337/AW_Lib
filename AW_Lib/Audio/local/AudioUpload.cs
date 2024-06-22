using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AW_Lib;
using NAudio;
using NAudio.Wave;


namespace AW_Lib.Audio.local
{



    public class Upload
    {
        public string URL { get; set; } = "Leer";
        public static string Path { get; set; } = "C:\\Users\\nfszo\\Desktop\\weck mich.wav";
        public DateTime currentDate { get; set; } = DateTime.Now;




        public static void File()
        { // URL oder lokaler Pfad
          // string audioPath = "C:/path/to/local/audiofile.mp3"; // Beispiel für lokalen Pfad

            // Lade die Audiodatei
            try
            {
                using (var audioFile = new AudioFileReader(Path))
                {
                    // Audio-Datei wurde erfolgreich geladen
                    Console.WriteLine("Audiodatei wurde erfolgreich geladen:");
                    Console.WriteLine($"Länge: {audioFile.TotalTime}");
                    Console.WriteLine($"Samplerate: {audioFile.WaveFormat.SampleRate} Hz");
                    Console.WriteLine($"Anzahl der Kanäle: {audioFile.WaveFormat.Channels}");
                    // Hier kannst du weitere Verarbeitungsschritte durchführen
                }
            }
            catch (Exception ex)
            {
                // Fehler beim Laden der Audiodatei
                Console.WriteLine($"Fehler beim Laden der Audiodatei: {ex.Message}");
            }
        }
    }



    // 

    class File
    {

    }



}
