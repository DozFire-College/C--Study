using System;

namespace Playlists
{


    public class Playlist
    {
        private string[] songs;
        private int count;
        
        public Playlist(string[] songsArray)
        {
            if (songsArray == null)
            {
                songs = new string[0];
                count = 0;
            }
            else
            {
                count = songsArray.Length;
                songs = new string[count];
                for (int i = 0; i < count; i++)
                {
                    songs[i] = songsArray[i];
                }
            }
        }

        private Playlist(int capacity)
        {
            songs = new string[capacity];
            count = capacity;
        }

        public static Playlist operator +(Playlist playlist1, Playlist playlist2)
        {
            int newSize = playlist1.count + playlist2.count;
            Playlist result = new Playlist(newSize);

            int index = 0;

            for (int i = 0; i < playlist1.count; i++)
            {
                result.songs[index] = playlist1.songs[i];
                index++;
            }

            for (int i = 0; i < playlist2.count; i++)
            {
                result.songs[index] = playlist2.songs[i];
                index++;
            }

            return result;
        }

        public static Playlist operator -(Playlist playlist, string song)
        {
            Playlist result = new Playlist(playlist.count + 1);

            for (int i = 0; i < playlist.count; i++)
            {
                result.songs[i] = playlist.songs[i];
            }

            result.songs[playlist.count] = song;

            return result;
        }
        public static Playlist operator ++(Playlist playlist)
        {
            Playlist result = new Playlist(playlist.count + 1);

            result.songs[0] = "New track";

            for (int i = 0; i < playlist.count; i++)
            {
                result.songs[i + 1] = playlist.songs[i];
            }

            return result;
        }
        public string[] GetSongs()
        {
            string[] copy = new string[count];
            for (int i = 0; i < count; i++)
            {
                copy[i] = songs[i];
            }

            return copy;
        }
        public int Count
        {
            get { return count; }
        }
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Индекс выходит за пределы плейлиста");
                return songs[index];
            }
        }
        public override string ToString()
        {
            if (count == 0)
                return "Плейлист пуст";

            string result = "";
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                    result += ", ";
                result += songs[i];
            }

            return result;
        }
        public void Display()
        {
            if (count == 0)
            {
                Console.WriteLine("Плейлист пуст");
                return;
            }

            Console.WriteLine("Плейлист:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1}. {songs[i]}");
            }
        }
    }
    class Program
    {
        static void Main()
        {
            var playlist1 = new Playlist(new string[] { "Мой плейлист", "Русские песни" });
            Console.WriteLine("Плейлист 1:");
            playlist1.Display();
            Console.WriteLine();

            var playlist2 = new Playlist(new string[] { "My playlist", "English music" });
            Console.WriteLine("Плейлист 2:");
            playlist2.Display();
            Console.WriteLine();
            
            var combinedPlaylist = playlist1 + playlist2;
            Console.WriteLine("Объединенный плейлист:");
            combinedPlaylist.Display();
            Console.WriteLine();
            
            combinedPlaylist = combinedPlaylist - "Yesterday";
            combinedPlaylist.Display();
            Console.WriteLine();
            
            combinedPlaylist = ++combinedPlaylist;
            combinedPlaylist.Display();
        }
    }
}