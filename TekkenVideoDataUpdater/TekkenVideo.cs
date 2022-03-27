using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploader
{
    internal class TekkenVideo
    {
        public int Number { get; set; }
        public string Character_en { get; set; }
        public string Character_kr { get; set; }
        public string Title_en { get; set; }
        public string Title_kr { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public TekkenVideo(int number, string Character_en, string Character_kr, string title_en, string title_kr, string fileName)
        {
            this.Number = number;
            this.Character_en = Character_en;
            this.Character_kr = Character_kr;
            this.Title_en = title_en;
            this.Title_kr = title_kr;
            Description = title_en + "\r\n" + title_kr;
            SetFilePath(fileName);

        }

        private void SetFilePath(string fileName)
        {
            this.FilePath = $"D:\\TEKKEN_PROJECT\\VIDEO\\{Number.ToString().PadLeft(2, '0')}.{Character_en}\\Moves\\01~10\\{fileName}"; // Replace with path to actual movie file.
        }
    }
}
