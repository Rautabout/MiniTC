using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalCommander.Model
{
    class FileModel
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _file;
        public string file
        {
            get { return _file; }
            set { _file = value; }
        }
        public FileModel(string file, string name)
        {
            this.file = file;
            this.name = name;
        }

    }
}
