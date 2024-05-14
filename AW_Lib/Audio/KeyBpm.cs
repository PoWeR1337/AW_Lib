using NAudio.Wave;
using System;

class Reader
{
    static void Main(string[] args)
    {
        // URL oder lokalen Pfad der Audiodatei angeben
        string audioPath = "http://example.com/audiofile.mp3"; // URL oder lokaler Pfad
        // string audioPath = "C:/path/to/local/audiofile.mp3"; // Beispiel für lokalen Pfad

        // Lade die Audiodatei
        try
        {
            using (var audioFile = new AudioFileReader(audioPath))
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
