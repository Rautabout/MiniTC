using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;

namespace TotalCommander.ViewModel
{
    using BaseClass;


    internal class ViewModelLogic : ViewModelBase
    {
        private readonly PanelTCViewModel _leftFilesViewModel;
        private readonly PanelTCViewModel _rightFilesViewModel;
        public PanelTCViewModel LeftFilesViewModel
        {
            get { return _leftFilesViewModel; }
        }

        public PanelTCViewModel RightFilesViewModel
        {
            get { return _rightFilesViewModel; }
        }

        public ViewModelLogic()
        {
            _rightFilesViewModel = new PanelTCViewModel();
            _leftFilesViewModel = new PanelTCViewModel();

        }

        #region Polecenia



        private ICommand _copy = null;
        private ICommand _delete = null;


        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(
                      arg => { CopyMet(); },
                      arg => (LeftFilesViewModel.pathName != null && RightFilesViewModel.pathName != null)
                    );


                }

                return _copy;
            }

        }

        private void CopyMet()
        {
            string fileName;
            string destFile;
            if (LeftFilesViewModel.File == null)
            {
                if (Directory.Exists(LeftFilesViewModel.pathName))
                {
                    string[] files = Directory.GetFiles(LeftFilesViewModel.pathName);

                    foreach (string s in files)
                    {
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(RightFilesViewModel.pathName, fileName);
                        File.Copy(s, destFile, true);
                    }
                }
            }
            else
            {
                fileName = LeftFilesViewModel.File.file;
                string s = Path.GetFileName(fileName);
                destFile = Path.Combine(RightFilesViewModel.pathName, s);
                LeftFilesViewModel.File = null;

                File.Copy(fileName, destFile, true);

            }


            LeftFilesViewModel.RefreshList(LeftFilesViewModel.pathName);
            RightFilesViewModel.RefreshList(RightFilesViewModel.pathName);

        }


        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommand(
                      arg => { DeleteMet(); },
                      arg => (LeftFilesViewModel.pathName != null && RightFilesViewModel.pathName != null)
                    ); 
                }

                return _delete;
            }

        }

        private void DeleteMet()
        {
            string fileName;
            string destFile;
            if (LeftFilesViewModel.File == null)
            {
                if (Directory.Exists(LeftFilesViewModel.pathName))
                {
                    string[] files = Directory.GetFiles(LeftFilesViewModel.pathName);

                    foreach (string s in files)
                    {
                        fileName = Path.GetFileName(s);
                        destFile = Path.Combine(RightFilesViewModel.pathName, fileName);
                        File.Delete(s);
                    }
                }
            }
            else
            {
                fileName = LeftFilesViewModel.File.file;
                string s = Path.GetFileName(fileName);
                destFile = Path.Combine(RightFilesViewModel.pathName, s);
                LeftFilesViewModel.File = null;

                File.Delete(fileName);

            }


            LeftFilesViewModel.RefreshList(LeftFilesViewModel.pathName);
            RightFilesViewModel.RefreshList(RightFilesViewModel.pathName);

        }
        #endregion
    }
}
