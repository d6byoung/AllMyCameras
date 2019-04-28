using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllMyCameras
{

    enum Mount
    {
        PK,
        SR,
        F,
        FD,
        EF,
        M,
        X,
        M42,
        PENF,
        FIXEDLENS
    }
    enum FinderType
    {
        RANGEFINDER,
        VIEWFINDER,
        SLR,
        TLR,
        VIEW
    }
    enum FilmFormat
    {
        FILMFULL, 
        FILMHALF,
        MEDIUMFORMAT,
        LARGEFORMAT,
        DIGITALFULL,
        DIGITALAPSC,
    }
    enum LensType
    {
        ULTRAWIDE,
        WIDE,
        NORMAL,
        TELE,
        SUPERTELE
    }
    enum ExposureType
    {
        MANUAL,
        APERTUREPREFERRED,
        SHUTTERPREFERRED,
        PROGRAM, 
        DONE
    }
    class Program
    #region CommentBlock
    // *************************************************************
    // Title: All My Cameras
    // Description: A stuff tracker with integrated calculator to keep track of my cameras and lenses. IO comming soon!
    // Application Type: Console, .NET framework
    // Author: Young, David
    // Dated Created: 20 April 2019
    // Last Modified: 28 April 2019
    // *************************************************************
    #endregion
    {
        static void Main(string[] args)
        {
            DisplayWelcome();
            Menu();
            SayGoodbye();
        }
        static void Menu()
        {
            string menuChoice;
            bool wantContinue = true;
            List<Camera> cameras = new List<Camera>();
            List<Lens> lenses = new List<Lens>();



            do
            {

                DisplayHeader("Main Menu");
                Console.WriteLine("1) Add Camera");
                Console.WriteLine("2) Remove Camera");
                Console.WriteLine("3) Add Lens");
                Console.WriteLine("4) Remove Lens");
                Console.WriteLine("5) Display");
                Console.WriteLine("6) Search");
                Console.WriteLine("7) Price Calculator");
                Console.WriteLine("E) Exit");
                Console.WriteLine();
                Console.Write("Enter Selection: ");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        AddCamera(cameras);
                        break;
                    case "2":
                        RemoveCamera(cameras);
                        break;
                    case "3":
                        AddLens(lenses);
                        break;
                    case "4":
                        RemoveLens(lenses);
                        break;
                    case "5":
                        DisplayMenu(cameras, lenses);
                        break;
                    case "6":
                        SearchMenu(cameras, lenses);
                        break;
                    case "7":
                        PriceCalc(cameras, lenses);
                        break;
                    case "e":
                    case "E":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }

            } while (wantContinue);
        }
        #region Calculator
        private static void PriceCalc(List<Camera> cameras, List<Lens> lenses)
        {
            string menuChoice;
            bool wantContinue = true;

            do
            {

                DisplayHeader("Price Calculator");
                Console.WriteLine("1) Total Value");
                Console.WriteLine("2) System Value");
                Console.WriteLine("E) Return");
                Console.WriteLine();
                Console.Write("Enter Selection: ");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        TotalCalc(cameras, lenses);
                        break;
                    case "2":
                        SystemCalc(cameras, lenses);
                        break;
                    case "e":
                    case "E":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }

            } while (wantContinue);
        }

        private static void SystemCalc(List<Camera> cameras, List<Lens> lenses)
        {
            double total = 0;
            Mount mount;

            DisplayHeader("System Value");

            Console.WriteLine("Enter Lens Mount:");
            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out mount))
            {
                Console.WriteLine("Unsupported mount!");
            }
            else
            {
                foreach (Camera camera in cameras)
                {
                    if (camera.Mount == mount)
                    {
                        DisplayCamera(camera);
                        Console.WriteLine();
                        total = total + camera.UnitValue;
                    }
                }
                foreach (Lens lens in lenses)
                {
                    if (lens.LensMount == mount)
                    {
                        DisplayLens(lens);
                        Console.WriteLine();
                        total = total + lens.UnitValue;
                    }
                }
            }

            Console.WriteLine($"Total Value: ${total}");
            WaitForUser();
        }

        private static void TotalCalc(List<Camera> cameras, List<Lens> lenses)
        {
            double total = 0;

            DisplayHeader("System Value");

            foreach (Camera camera in cameras)
            {
                DisplayCamera(camera);
                Console.WriteLine();
                total = total + camera.UnitValue;
            }
            foreach (Lens lens in lenses)
            {
                DisplayLens(lens);
                Console.WriteLine();
                total = total + lens.UnitValue;
            }

            Console.WriteLine($"Total Value: ${total}");
            WaitForUser();
        }
        #endregion
        #region Search
        static void SearchMenu(List<Camera> cameras, List<Lens> lenses)
        {
            string menuChoice;
            bool wantContinue = true;
            
            do
            {
                DisplayHeader("Search Menu");
                Console.WriteLine("1) Search Cameras");
                Console.WriteLine("2) Search Lenses");
                Console.WriteLine("E) return");
                Console.WriteLine();
                Console.WriteLine("Enter Selection");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        SearchCameras(cameras);
                        break;
                    case "2":
                        SearchLenses(lenses);
                        break;
                    case "E":
                    case "e":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }
            } while (wantContinue);
        }
        #region LensSearch
        static void SearchLenses(List<Lens> lenses)
        {
            string menuChoice;
            bool wantContinue = true;
            
            do
            {
                DisplayHeader("Search Lenses");
                Console.WriteLine("1) Search by make");
                Console.WriteLine("2) Search by year");
                Console.WriteLine("3) Search by type");
                Console.WriteLine("4) Search by mount");
                Console.WriteLine("5) Search by aperture");
                Console.WriteLine("E) return");
                Console.WriteLine();
                Console.WriteLine("Enter Selection");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        SearchLensMake(lenses);
                        break;
                    case "2":
                        SearchLensYear(lenses);
                        break;
                    case "3":
                        SearchLensType(lenses);
                        break;
                    case "4":
                        SearchLensMount(lenses);
                        break;
                    case "5":
                        SearchLensAperture(lenses);
                        break;
                    case "E":
                    case "e":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }
            } while (wantContinue);
        }

        static void SearchLensAperture(List<Lens> lenses)
        {
            DisplayHeader("Lenses by Aperture");
            Console.WriteLine("Enter Aperture:");

            if (!double.TryParse(Console.ReadLine(), out double aperture))
            {
                Console.WriteLine("Invalid aperture!");
            }
            else
            {
                foreach (Lens lens in lenses)
                {
                    if (lens.Aperture == aperture)
                    {
                        DisplayLens(lens);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchLensMount(List<Lens> lenses)
        {
            DisplayHeader("Lenses by Mount");
            Console.WriteLine("Enter Mount:");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Mount mount))
            {
                Console.WriteLine("Unsupported mount!");
            }
            else
            {
                foreach (Lens lens in lenses)
                {
                    if (lens.LensMount == mount)
                    {
                        DisplayLens(lens);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchLensType(List<Lens> lenses)
        {
            DisplayHeader("Lenses by Type");
            Console.WriteLine("Enter Type:");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out LensType type))
            {
                Console.WriteLine("Unsupported lens type.");
            }
            else
            {
                foreach (Lens lens in lenses)
                {
                    if (lens.LensType == type)
                    {
                        DisplayLens(lens);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchLensYear(List<Lens> lenses)
        {
            DisplayHeader("Lenses by Year");
            Console.WriteLine("Enter Year:");

            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year!");
            }
            else
            {
                foreach (Lens lens in lenses)
                {
                    if (lens.Year == year)
                    {
                        DisplayLens(lens);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchLensMake(List<Lens> lenses)
        {
            DisplayHeader("Lenses by Make");
            Console.WriteLine("Enter Make:");

            string make = Console.ReadLine();

            foreach (Lens lens in lenses)
            {
                if (lens.Make == make)
                {
                    DisplayLens(lens);
                    Console.WriteLine();
                }
            }
            WaitForUser();
        }
        #endregion
        #region CamSearch
        static void SearchCameras(List<Camera> cameras)
        {
            string menuChoice;
            bool wantContinue = true;
            
            do
            {
                DisplayHeader("Search Cameras");
                Console.WriteLine("1) Search by make");
                Console.WriteLine("2) Search by year");
                Console.WriteLine("3) Search by finder style");
                Console.WriteLine("4) Search by Format");
                Console.WriteLine("5) Search by Lens Mount");
                Console.WriteLine("E) return");
                Console.WriteLine();
                Console.WriteLine("Enter Selection");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        SearchCamMake(cameras);
                        break;
                    case "2":
                        SearchCamYear(cameras);
                        break;
                    case "3":
                        SearchCamFinder(cameras);
                        break;
                    case "4":
                        SearchCamFormat(cameras);
                        break;
                    case "5":
                        SearchCamMount(cameras);
                        break;
                    case "E":
                    case "e":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }
            } while (wantContinue);
        }

        static void SearchCamMount(List<Camera> cameras)
        {
            DisplayHeader("Cameras by Mount");
            Console.WriteLine("Enter Mount:");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Mount mount))
            {
                Console.WriteLine("Invalid Mount!");
            }
            else
            {
                foreach (Camera camera in cameras)
                {
                    if (camera.Mount == mount)
                    {
                        DisplayCamera(camera);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchCamFormat(List<Camera> cameras)
        {
            DisplayHeader("Cameras by Format");
            Console.WriteLine("Enter Format:");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out FilmFormat format))
            {
                Console.WriteLine("Invalid Format!");
            }
            else
            {
                foreach (Camera camera in cameras)
                {
                    if (camera.FilmFormat == format)
                    {
                        DisplayCamera(camera);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchCamFinder(List<Camera> cameras)
        {
            DisplayHeader("Cameras by Finder Type");
            Console.WriteLine("Enter Finder Type:");

            if (!Enum.TryParse(Console.ReadLine().ToUpper(), out FinderType finder))
            {
                Console.WriteLine("Invalid Finder Type!");
            }
            else
            {
                foreach (Camera camera in cameras)
                {
                    if (camera.FinderType == finder)
                    {
                        DisplayCamera(camera);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchCamYear(List<Camera> cameras)
        {
            DisplayHeader("Cameras by Year");
            Console.WriteLine("Enter Year:");

            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Invalid year!");
            }
            else
            {
                foreach (Camera camera in cameras)
                {
                    if (camera.Year == year)
                    {
                        DisplayCamera(camera);
                        Console.WriteLine();
                    }
                }
            }
            WaitForUser();
        }

        static void SearchCamMake(List<Camera> cameras)
        {
            DisplayHeader("Cameras by Make");
            Console.WriteLine("Enter Make:");

            string make = Console.ReadLine();

            foreach (Camera camera in cameras)
            {
                if (camera.Make == make)
                {
                    DisplayCamera(camera);
                    Console.WriteLine();
                }
            }
            WaitForUser();
        }
        #endregion
        #endregion
        #region Display
        static void DisplayMenu(List<Camera> cameras, List<Lens> lenses)
        {
            string menuChoice = "0";
            bool wantContinue = true;
            
            do
            {
                DisplayHeader("Show Inventory");
                Console.WriteLine("1) Cameras");
                Console.WriteLine("2) Lenses");
                Console.WriteLine("3) Cameras and Lenses");
                Console.WriteLine("E) return");
                Console.WriteLine();
                Console.WriteLine("Enter Selection");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        DisplayCameras(cameras);
                        break;
                    case "2":
                        DisplayLenses(lenses);
                        break;
                    case "3":
                        DisplayCameras(cameras);
                        DisplayLenses(lenses);
                        break;
                    case "E":
                    case "e":
                        wantContinue = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response. Please try again.");
                        WaitForUser();
                        break;
                }
            } while (wantContinue);
        }

        static void DisplayLenses(List<Lens> lenses)
        {
            DisplayHeader("Lenses");
            
            foreach (Lens lens in lenses)
            {
                DisplayLens(lens);
                Console.WriteLine();
            }

            WaitForUser();
        }

        static void DisplayLens(Lens lens)
        {
            Console.WriteLine($"{lens.Make} {lens.Model}");
            Console.WriteLine($"{lens.Focal}mm f/{lens.Aperture}");
            Console.WriteLine($"Made {lens.Year} for {lens.LensMount} mount");
            Console.WriteLine(lens.LensType);
            Console.WriteLine($"${lens.UnitValue}");
        }

        static void DisplayCameras(List<Camera> cameras)
        {
            DisplayHeader("Cameras");

            foreach (Camera camera in cameras)
            {
                DisplayCamera(camera);
                Console.WriteLine();
            }

            WaitForUser();
        }

        static void DisplayCamera(Camera camera)
        {
            Console.WriteLine($"{camera.Make} {camera.Model}");
            Console.WriteLine($"{camera.FilmFormat} {camera.FinderType}");
            Console.WriteLine($"Made {camera.Year} in {camera.Mount} mount");
            Console.WriteLine($"${camera.UnitValue}");
        }
        #endregion
        #region AddRemove
        static void RemoveLens(List<Lens> lenses)
        {
            bool valid = false;
            int focal;
            double aperture;

            DisplayHeader("Remove Lens");

            foreach (Lens lens in lenses)
            {
                Console.WriteLine($"Make: {lens.Make}   Model:{lens.Model}   Focal:{lens.Focal}   Aperture:{lens.Aperture}");
            }

            Console.WriteLine("Which lens would you like to remove? Enter Make:");
            string worstBrand = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            string worstModel = Console.ReadLine();

            //Get Focal
            do
            {
                Console.WriteLine("Enter Focal:");
                if (!int.TryParse(Console.ReadLine(), out focal))
                {
                    Console.WriteLine("Invalid focal length");
                    valid = false;
                }
                else
                {
                    valid = true;
                }
            } while (!valid);

            //Get Aperture
            do
            {
                Console.WriteLine("Enter Aperture:");
                if (!double.TryParse(Console.ReadLine(), out aperture))
                {
                    Console.WriteLine("Invalid aperture");
                    valid = false;
                }
                else
                {
                    valid = true;
                }
            } while (!valid);
            
            foreach (Lens lens in lenses)
            {
                if (lens.Make == worstBrand && lens.Model == worstModel && lens.Focal == focal && lens.Aperture == aperture)
                {
                    Console.WriteLine($"{lens.Make} {lens.Model} {lens.Focal}mm f/{lens.Aperture} has been removed.");
                    lenses.Remove(lens);
                    break;
                }
            }

            WaitForUser();
        }

        static void RemoveCamera(List<Camera> cameras)
        {
            DisplayHeader("Remove Camera");

            foreach (Camera camera in cameras)
            {
                Console.WriteLine($"Make: {camera.Make} Model :{camera.Model}");
            }

            Console.WriteLine("Which camera would you like to remove? Enter Make:");
            string worstBrand = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            string worstModel = Console.ReadLine();

            foreach (Camera camera in cameras)
            {
                if (camera.Make == worstBrand && camera.Model == worstModel)
                {
                    Console.WriteLine($"{camera.Make} {camera.Model} has been removed.");
                    cameras.Remove(camera);
                    break;
                }
            }

            WaitForUser();
        }

        static void AddLens(List<Lens> lenses)
        {
            DisplayHeader("Add a Lens");

            bool valid = false;
            Lens newLens = new Lens();

            Console.WriteLine("Enter Make:");
            newLens.Make = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            newLens.Model = Console.ReadLine();

            //Enter Year
            do
            {
                Console.WriteLine("Enter Year:");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Please enter a valid year (integer)");
                    valid = false;
                }
                else
                {
                    newLens.Year = year;
                    valid = true;
                }
            } while (!valid);

            //Enter Focal
            do
            {
                Console.WriteLine("Enter Focal Length:");
                if (!int.TryParse(Console.ReadLine(), out int focal))
                {
                    Console.WriteLine("Please enter a valid focal length (integer)");
                    valid = false;
                }
                else
                {
                    newLens.Focal = focal;
                    valid = true;
                }
            } while (!valid);

            //Enter Aperture
            do
            {
                Console.WriteLine("Enter Lens Aperture");
                if (!double.TryParse(Console.ReadLine(), out double aperture))
                {
                    Console.WriteLine("Please enter a valid aperture value");
                    valid = false;
                }
                else
                {
                    newLens.Aperture = aperture;
                    valid = true;
                }
            } while (!valid);

            //Enter Type
            do
            {
                Console.WriteLine("Enter Lens Type:");
                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out LensType type))
                {
                    Console.WriteLine("Please enter a supported lens type");
                    valid = false;
                }
                else
                {
                    newLens.LensType = type;
                    valid = true;
                }
            } while (!valid);

            //Enter Mount
            do
            {
                Console.WriteLine("Enter Lens Mount:");
                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Mount mount))
                {
                    Console.WriteLine("Please enter a supported lens mount");
                    valid = false;
                }
                else
                {
                    newLens.LensMount = mount;
                    valid = true;
                }
            } while (!valid);

            //Enter Value
            do
            {
                Console.WriteLine("Enter Value:");
                if (!double.TryParse(Console.ReadLine(), out double value))
                {
                    Console.WriteLine("Please enter a valid value (numeric)");
                    valid = false;
                }
                else
                {
                    newLens.UnitValue = value;
                    valid = true;
                }
            } while (!valid);
            
            lenses.Add(newLens);
            if (lenses.Contains(newLens))
            {
                Console.WriteLine("Lens Added!");
            }
            else
            {
                Console.WriteLine("Error: lens not added.");
            }
            WaitForUser();
        }

        static void AddCamera(List<Camera> cameras)
        {
            bool valid = false;

            DisplayHeader("Add a Camera");

            Camera newCamera = new Camera();

            Console.WriteLine("Enter Make:");
            newCamera.Make = Console.ReadLine();

            Console.WriteLine("Enter Model:");
            newCamera.Model = Console.ReadLine();

            //Enter Year
            do
            {
                Console.WriteLine("Enter Year:");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("Please enter a valid year (integer)");
                    valid = false;
                }
                else
                {
                    newCamera.Year = year;
                    valid = true;
                }
            } while (!valid);

            //Enter Finder
            do
            {
                Console.WriteLine("Enter Finder Type:");
                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out FinderType finder))
                {
                    Console.WriteLine("Please enter a supported finder type");
                    valid = false;
                }
                else
                {
                    newCamera.FinderType = finder;
                    valid = true;
                }
            } while (!valid);

            //Enter Format
            do
            {
                Console.WriteLine("Enter Film Format:");
                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out FilmFormat format))
                {
                    Console.WriteLine("Please enter a supported film format");
                    valid = false;
                }
                else
                {
                    newCamera.FilmFormat = format;
                    valid = true;
                }
            } while (!valid);

            //Enter Mount
            do
            {
                Console.WriteLine("Enter Lens Mount:");
                if (!Enum.TryParse(Console.ReadLine().ToUpper(), out Mount mount))
                {
                    Console.WriteLine("Please enter a supported lens mount");
                    valid = false;
                }
                else
                {
                    newCamera.Mount = mount;
                    valid = true;
                }
            } while (!valid);

            //Enter Value
            do
            {
                Console.WriteLine("Enter Value:");
                if (!double.TryParse(Console.ReadLine(), out double value))
                {
                    Console.WriteLine("Please enter a valid value (numeric)");
                    valid = false;
                }
                else
                {
                    newCamera.UnitValue = value;
                    valid = true;
                }
            } while (!valid);
            
            cameras.Add(newCamera);
            if (cameras.Contains(newCamera))
            {
                Console.WriteLine("Camera Added!");
            }
            else
            {
                Console.WriteLine("Error: camera not added.");
            }
            WaitForUser();

        }
        #endregion
        #region Logistics
        static void DisplayWelcome()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Welcome to All My Cameras, a special stuff tracker for shutterbugs.");
            Console.WriteLine("This app will allow you to keep track of all your cameras and the lenses that come with them.");
            WaitForUser();
        }
        static void WaitForUser()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        static void DisplayHeader(string headerText)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(headerText);
            Console.WriteLine();
        }
        static void SayGoodbye()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Goodbye forever!");
            WaitForUser();
        }
        #endregion
    }
    class Camera
    {
        #region FIELDS

        private FinderType _finderType;

        private FilmFormat _filmFormat;

        private int _year;

        private double _unitValue;

        private string _make;

        private string _model;

        private Mount _mount;

        private List<ExposureType> _exposure = new List<ExposureType>();

        #endregion

        #region PROPERTIES

        public FinderType FinderType
        {
            get { return _finderType; }
            set { _finderType = value; }
        }
        public FilmFormat FilmFormat
        {
            get { return _filmFormat; }
            set { _filmFormat = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public double UnitValue
        {
            get { return _unitValue; }
            set { _unitValue = value; }
        }
        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public Mount Mount
        {
            get { return _mount; }
            set { _mount = value; }
        }
        public List<ExposureType> Exposure
        {
            get { return _exposure; }
            set { _exposure = value; }
        }
        #endregion

        #region METHODS

        #endregion

        #region CONSTRUCTORS

        public Camera()
        {

        }

        public Camera(FinderType finderType, FilmFormat filmFormat, int year, double unitValue, string make, string model, Mount mount, List<ExposureType> exposure)
        {
            _finderType = finderType;
            _filmFormat = filmFormat;
            _year = year;
            _unitValue = unitValue;
            _make = make;
            _model = model;
            _mount = mount;
            _exposure = exposure;
        }

        #endregion
    }
    class Lens
    {
        #region FIELDS

        private LensType _lensType;

        private Mount _lensmount;

        private int _year;

        private int _focal;

        private double _aperture;

        private double _unitValue;

        private string _make;

        private string _model;

        #endregion

        #region PROPERTIES

        public LensType LensType
        {
            get { return _lensType; }
            set { _lensType = value; }
        }
        public Mount LensMount
        {
            get { return _lensmount; }
            set { _lensmount = value; }
        }
        public int Focal
        {
            get { return _focal; }
            set { _focal = value; }
        }
        public double Aperture
        {
            get { return _aperture; }
            set { _aperture = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public double UnitValue
        {
            get { return _unitValue; }
            set { _unitValue = value; }
        }
        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        #endregion

        #region METHODS

        #endregion

        #region CONSTRUCTORS

        public Lens()
        {

        }

        public Lens(LensType lensType, Mount lensmount, int focal, double aperture, int year, double unitValue, string make, string model)
        {
            _lensType = lensType;
            _lensmount = lensmount;
            _focal = focal;
            _aperture = aperture;
            _year = year;
            _unitValue = unitValue;
            _make = make;
            _model = model;
        }

        #endregion
    }
}
