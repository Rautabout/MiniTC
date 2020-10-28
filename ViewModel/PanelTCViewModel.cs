using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TotalCommander.Model;
using TotalCommander.ViewModel.BaseClass;

namespace TotalCommander.ViewModel
{
    class PanelTCViewModel : ViewModelBase
    {
        private ObservableCollection<FileModel> _files;

        public ObservableCollection<FileModel> Files
        {
            get { return _files; }
            set { SetField(ref _files, value); }
        }

        private ObservableCollection<DriveModel> _drives;

        public ObservableCollection<DriveModel> Drives
        {
            get { return _drives; }
            set { SetField(ref _drives, value); }
        }

        private DriveModel _selectedDrive;

        public DriveModel selectedDrive
        {
            get { return _selectedDrive; }
            set
            {
                _selectedDrive = value;
            }
        }

        private FileModel _file;

        public FileModel File
        {
            get { return _file; }
            set { _file = value; }

        }

        private string _pathName;

        public string pathName
        {
            get { return _pathName; }
            set
            {
                _pathName = value;
                onPropertyChanged(nameof(pathName));
            }
        }


        public PanelTCViewModel()
        {
            _drives = new ObservableCollection<DriveModel>();


            AddDrives(_drives);
        }


        private bool TryOpen(string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                return true;
            }
            catch { return false; }


        }


        public void RefreshList(string path)
        {
            pathName = path;
            Files = new ObservableCollection<FileModel>();
            if (Directory.GetParent(path) != null)
            {
                Files.Add(new FileModel(Directory.GetParent(path).FullName, "..."));
            }
            string[] dirs = Directory.GetDirectories(path);
            string[] name = Directory.GetFiles(path);

            foreach (string x in dirs)
            {
                Files.Add(new FileModel(x, "<d>" + x.Substring(3)));
            }

            foreach (string x in name)
            {
                Files.Add(new FileModel(x, x.Substring(3)));
            }


        }

        public void AddDrives(ObservableCollection<DriveModel> drivesCollection)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    drivesCollection.Add(new DriveModel()
                    {
                        Name = d.Name,

                    });
                }
            }
        }

        private ICommand _update = null;

        public ICommand Update
        {
            get
            {
                if (_update == null)
                {
                    _update = new RelayCommand(
                    arg => { RefreshList(selectedDrive.Name); },
                    arg => (selectedDrive != null)


                     );


                }

                return _update;
            }

        }

        private ICommand _updateList = null;

        public ICommand UpdateList
        {
            get
            {
                if (_updateList == null)
                {
                    _updateList = new RelayCommand(
                    arg => { RefreshList(File.file); },
                    arg => (File != null && (File.name.Contains("<d>") || File.name.Contains("...")) && TryOpen(File.file))


                     );


                }

                return _updateList;
            }

        }
    }
}
