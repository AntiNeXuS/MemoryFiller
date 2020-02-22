using CommonServiceLocator;
using CpuMemStresser.ViewModel;
using GalaSoft.MvvmLight.Ioc;

namespace MemoryFiller.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainWindowVm>();
        }

        public MainWindowVm Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainWindowVm>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
