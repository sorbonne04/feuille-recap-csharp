using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpCheatSheet
{
    // ============================================
    // CLASSES ET MODIFICATEURS D'ACCÈS
    // ============================================

    // Classe publique (accessible partout)
    public class PublicClass
    {
        // Champs publics (accessibles partout)
        public string PublicField = "accessible partout";

        // Champs privés (accessibles uniquement dans cette classe)
        private string privateField = "accessible uniquement ici";

        // Champs protégés (accessibles dans cette classe et ses dérivées)
        protected string protectedField = "accessible dans classe et dérivées";

        // Champs internes (accessibles dans le même assembly)
        internal string internalField = "accessible dans le même assembly";
    }

    // Classe privée (accessible uniquement dans ce fichier)
    class PrivateClass { }

    // Classe statique (ne peut pas être instanciée)
    public static class StaticClass
    {
        public static void StaticMethod() { }
    }

    // ============================================
    // PROPRIÉTÉS (PROPERTIES)
    // ============================================

    public class Properties
    {
        // Propriété auto-implémentée
        public string Name { get; set; }

        // Propriété avec valeur par défaut
        public int Age { get; set; } = 0;

        // Propriété en lecture seule
        public string ReadOnlyProp { get; }

        // Propriété en écriture seule (rare)
        private string _password;
        public string Password { set => _password = value; }

        // Propriété avec getter et setter personnalisés
        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                if (value >= 0)
                    _count = value;
            }
        }

        // Propriété calculée (expression-bodied)
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Propriété avec modificateur d'accès différent
        public string Id { get; private set; }

        // Propriété init (C# 9.0+) - assignable uniquement à l'initialisation
        public string Country { get; init; }

        public Properties()
        {
            ReadOnlyProp = "Initialisé dans constructeur";
            Id = Guid.NewGuid().ToString();
        }
    }

    // ============================================
    // CONSTRUCTEURS
    // ============================================

    public class Constructors
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // Constructeur par défaut
        public Constructors()
        {
            Name = "Anonyme";
            Age = 0;
        }

        // Constructeur avec paramètres
        public Constructors(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // Constructeur avec chaînage (this)
        public Constructors(string name) : this(name, 0)
        {
        }

        // Constructeur statique (appelé une seule fois)
        static Constructors()
        {
            Console.WriteLine("Constructeur statique appelé");
        }
    }

    // ============================================
    // HÉRITAGE ET POLYMORPHISME
    // ============================================

    // Classe de base
    public class Animal
    {
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }

        // Méthode virtuelle (peut être redéfinie)
        public virtual void MakeSound()
        {
            Console.WriteLine("Animal fait un son");
        }

        // Méthode normale
        public void Eat()
        {
            Console.WriteLine($"{Name} mange");
        }
    }

    // Classe dérivée
    public class Dog : Animal
    {
        public string Breed { get; set; }

        // Appel du constructeur de base avec base()
        public Dog(string name, string breed) : base(name)
        {
            Breed = breed;
        }

        // Override: redéfinit la méthode virtuelle
        public override void MakeSound()
        {
            Console.WriteLine("Woof! Woof!");
        }

        // Méthode supplémentaire
        public void Fetch()
        {
            Console.WriteLine($"{Name} rapporte la balle");
        }
    }

    // Classe abstraite (ne peut pas être instanciée)
    public abstract class Shape
    {
        public string Color { get; set; }

        // Méthode abstraite (doit être implémentée)
        public abstract double GetArea();

        // Méthode normale
        public void Display()
        {
            Console.WriteLine($"Forme de couleur {Color}");
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    // Classe sealed (ne peut pas être héritée)
    public sealed class FinalClass
    {
        public void Method() { }
    }

    // ============================================
    // INTERFACES
    // ============================================

    public interface IVehicle
    {
        // Propriétés
        string Brand { get; set; }
        int Speed { get; set; }

        // Méthodes
        void Start();
        void Stop();
        void Accelerate(int amount);
    }

    // Une classe peut implémenter plusieurs interfaces
    public interface IElectric
    {
        int BatteryLevel { get; set; }
        void Charge();
    }

    public class ElectricCar : IVehicle, IElectric
    {
        public string Brand { get; set; }
        public int Speed { get; set; }
        public int BatteryLevel { get; set; }

        public void Start()
        {
            Console.WriteLine("Moteur électrique démarré");
        }

        public void Stop()
        {
            Console.WriteLine("Moteur arrêté");
        }

        public void Accelerate(int amount)
        {
            Speed += amount;
        }

        public void Charge()
        {
            BatteryLevel = 100;
            Console.WriteLine("Batterie chargée à 100%");
        }
    }

    // ============================================
    // ENUMS (ÉNUMÉRATIONS)
    // ============================================

    public class Enums
    {
        // Enum simple
        public enum DayOfWeek
        {
            Monday,      // 0
            Tuesday,     // 1
            Wednesday,   // 2
            Thursday,    // 3
            Friday,      // 4
            Saturday,    // 5
            Sunday       // 6
        }

        // Enum avec valeurs personnalisées
        public enum HttpStatusCode
        {
            OK = 200,
            NotFound = 404,
            ServerError = 500
        }

        // Enum avec attribut Flags pour combinaisons
        [Flags]
        public enum FilePermissions
        {
            None = 0,
            Read = 1,
            Write = 2,
            Execute = 4,
            ReadWrite = Read | Write,
            All = Read | Write | Execute
        }

        public void UseEnums()
        {
            // Utilisation
            DayOfWeek today = DayOfWeek.Monday;

            // Conversion en int
            int dayNumber = (int)today;                     // 0

            // Conversion de string
            DayOfWeek parsed = Enum.Parse<DayOfWeek>("Tuesday");

            // TryParse (plus sûr)
            if (Enum.TryParse<DayOfWeek>("Wednesday", out DayOfWeek day))
            {
                Console.WriteLine($"Jour parsé: {day}");
            }

            // Obtenir tous les noms
            string[] names = Enum.GetNames<DayOfWeek>();

            // Obtenir toutes les valeurs
            DayOfWeek[] values = Enum.GetValues<DayOfWeek>();

            // Flags
            FilePermissions perms = FilePermissions.Read | FilePermissions.Write;
            bool canRead = perms.HasFlag(FilePermissions.Read);  // true
        }
    }

    // ============================================
    // STRUCTS (STRUCTURES)
    // ============================================

    // Struct: type valeur (contrairement aux classes qui sont des types référence)
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceFromOrigin()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
    }

    // Record struct (C# 10.0+)
    public record struct Rectangle(int Width, int Height);

    // ============================================
    // RECORDS (C# 9.0+)
    // ============================================

    // Record: type référence immuable par défaut
    public record Person(string FirstName, string LastName, int Age);

    // Record avec corps
    public record Employee(string Name, decimal Salary)
    {
        public string Department { get; init; }

        public decimal AnnualBonus => Salary * 0.1m;
    }

    public class RecordExamples
    {
        public void UseRecords()
        {
            // Création
            Person person1 = new Person("Alice", "Dupont", 30);

            // With expression (crée une copie avec modifications)
            Person person2 = person1 with { Age = 31 };

            // Égalité par valeur (pas par référence)
            Person person3 = new Person("Alice", "Dupont", 30);
            bool areEqual = person1 == person3;              // true

            // Déconstruction
            var (firstName, lastName, age) = person1;
        }
    }

    // ============================================
    // DELEGATES ET EVENTS
    // ============================================

    public class DelegatesAndEvents
    {
        // Delegate: pointeur vers une méthode
        public delegate int MathOperation(int a, int b);

        // Delegate générique Action (ne retourne rien)
        public void UseAction()
        {
            Action<string> printMessage = (message) => Console.WriteLine(message);
            printMessage("Hello");

            Action<int, int> printSum = (a, b) => Console.WriteLine(a + b);
            printSum(5, 3);
        }

        // Delegate générique Func (retourne une valeur)
        public void UseFunc()
        {
            Func<int, int, int> add = (a, b) => a + b;
            int result = add(5, 3);                         // 8

            Func<string, bool> isEmpty = (s) => string.IsNullOrEmpty(s);
            bool empty = isEmpty("");                       // true
        }

        // Predicate (retourne bool)
        public void UsePredicate()
        {
            Predicate<int> isEven = (n) => n % 2 == 0;
            bool result = isEven(4);                        // true
        }

        // Events
        public class Button
        {
            // Déclaration d'event
            public event EventHandler Clicked;

            public void Click()
            {
                // Déclencher l'event
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void UseEvents()
        {
            Button btn = new Button();

            // S'abonner à l'event
            btn.Clicked += (sender, args) => Console.WriteLine("Bouton cliqué!");

            // Déclencher
            btn.Click();
        }
    }

    // ============================================
    // LAMBDA EXPRESSIONS
    // ============================================

    public class LambdaExpressions
    {
        public void BasicLambdas()
        {
            // Lambda sans paramètre
            Action greet = () => Console.WriteLine("Hello");

            // Lambda avec un paramètre
            Action<string> sayHello = name => Console.WriteLine($"Hello {name}");

            // Lambda avec plusieurs paramètres
            Func<int, int, int> add = (a, b) => a + b;

            // Lambda avec corps
            Func<int, int, int> multiply = (a, b) =>
            {
                int result = a * b;
                Console.WriteLine($"{a} * {b} = {result}");
                return result;
            };
        }

        public void LambdaWithLinq()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            // Filtrer avec lambda
            var evenNumbers = numbers.Where(n => n % 2 == 0);

            // Transformer avec lambda
            var squares = numbers.Select(n => n * n);

            // Trier avec lambda
            var sorted = numbers.OrderBy(n => n);
        }
    }

    // ============================================
    // FICHIERS ET I/O
    // ============================================

    public class FileOperations
    {
        public void ReadFile()
        {
            // Lire tout le fichier
            string content = File.ReadAllText("fichier.txt");

            // Lire toutes les lignes
            string[] lines = File.ReadAllLines("fichier.txt");

            // Lire ligne par ligne (économise mémoire)
            foreach (string line in File.ReadLines("fichier.txt"))
            {
                Console.WriteLine(line);
            }

            // Lire avec StreamReader
            using (StreamReader reader = new StreamReader("fichier.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        public void WriteFile()
        {
            // Écrire tout le texte (écrase le fichier)
            File.WriteAllText("fichier.txt", "Contenu du fichier");

            // Écrire toutes les lignes
            string[] lines = { "Ligne 1", "Ligne 2", "Ligne 3" };
            File.WriteAllLines("fichier.txt", lines);

            // Ajouter à la fin du fichier
            File.AppendAllText("fichier.txt", "Nouvelle ligne\n");

            // Écrire avec StreamWriter
            using (StreamWriter writer = new StreamWriter("fichier.txt"))
            {
                writer.WriteLine("Première ligne");
                writer.WriteLine("Deuxième ligne");
            }
        }

        public void FileManipulation()
        {
            // Vérifier si existe
            bool exists = File.Exists("fichier.txt");

            // Copier
            File.Copy("source.txt", "destination.txt");

            // Déplacer/Renommer
            File.Move("ancien.txt", "nouveau.txt");

            // Supprimer
            File.Delete("fichier.txt");

            // Obtenir infos
            FileInfo info = new FileInfo("fichier.txt");
            long size = info.Length;
            DateTime created = info.CreationTime;
        }

        public void DirectoryOperations()
        {
            // Créer dossier
            Directory.CreateDirectory("MonDossier");

            // Vérifier si existe
            bool exists = Directory.Exists("MonDossier");

            // Lister fichiers
            string[] files = Directory.GetFiles("MonDossier");

            // Lister sous-dossiers
            string[] dirs = Directory.GetDirectories("MonDossier");

            // Lister tout (fichiers et dossiers)
            string[] all = Directory.GetFileSystemEntries("MonDossier");

            // Supprimer dossier
            Directory.Delete("MonDossier", recursive: true);

            // Chemin actuel
            string currentDir = Directory.GetCurrentDirectory();
        }

        public void PathOperations()
        {
            // Combiner chemins
            string path = Path.Combine("dossier", "sous-dossier", "fichier.txt");

            // Obtenir parties du chemin
            string fileName = Path.GetFileName(path);           // "fichier.txt"
            string fileNameNoExt = Path.GetFileNameWithoutExtension(path); // "fichier"
            string extension = Path.GetExtension(path);         // ".txt"
            string directory = Path.GetDirectoryName(path);     // "dossier\sous-dossier"

            // Chemin temporaire
            string tempPath = Path.GetTempPath();
            string tempFile = Path.GetTempFileName();
        }
    }

    // ============================================
    // EXPRESSIONS RÉGULIÈRES (REGEX)
    // ============================================

    public class RegularExpressions
    {
        public void BasicRegex()
        {
            string text = "Mon email est test@example.com";

            // Vérifier si correspond
            bool isMatch = Regex.IsMatch(text, @"\w+@\w+\.\w+");

            // Trouver première correspondance
            Match match = Regex.Match(text, @"\w+@\w+\.\w+");
            if (match.Success)
            {
                Console.WriteLine($"Email trouvé: {match.Value}");
            }

            // Trouver toutes les correspondances
            string emails = "Emails: test1@example.com, test2@example.com";
            MatchCollection matches = Regex.Matches(emails, @"\w+@\w+\.\w+");
            foreach (Match m in matches)
            {
                Console.WriteLine(m.Value);
            }

            // Remplacer
            string censored = Regex.Replace(text, @"\w+@\w+\.\w+", "[EMAIL]");

            // Diviser
            string data = "Alice,Bob,Charlie";
            string[] names = Regex.Split(data, @",\s*");
        }

        public void CommonPatterns()
        {
            // Email
            string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";

            // Téléphone (français)
            string phonePattern = @"^0[1-9](\d{2}){4}$";

            // Code postal (français)
            string zipPattern = @"^\d{5}$";

            // URL
            string urlPattern = @"https?://[\w\.-]+\.\w+(/[\w\.-]*)*";

            // Date (JJ/MM/AAAA)
            string datePattern = @"^\d{2}/\d{2}/\d{4}$";

            // Numéro de carte bancaire
            string cardPattern = @"^\d{4}[\s-]?\d{4}[\s-]?\d{4}[\s-]?\d{4}$";
        }
    }

    // ============================================
    // ASYNC/AWAIT (PROGRAMMATION ASYNCHRONE)
    // ============================================

    public class AsyncProgramming
    {
        // Méthode asynchrone
        public async Task<string> DownloadDataAsync(string url)
        {
            // Simuler téléchargement
            await Task.Delay(1000);
            return "Données téléchargées";
        }

        // Méthode async qui ne retourne rien
        public async Task ProcessDataAsync()
        {
            await Task.Delay(500);
            Console.WriteLine("Traitement terminé");
        }

        // Utilisation
        public async Task UseAsyncMethods()
        {
            // Attendre une tâche
            string data = await DownloadDataAsync("http://example.com");
            Console.WriteLine(data);

            // Exécuter plusieurs tâches en parallèle
            Task<string> task1 = DownloadDataAsync("url1");
            Task<string> task2 = DownloadDataAsync("url2");

            // Attendre toutes les tâches
            string[] results = await Task.WhenAll(task1, task2);

            // Attendre la première tâche terminée
            Task<string> firstCompleted = await Task.WhenAny(task1, task2);
        }

        // ConfigureAwait
        public async Task UseConfigureAwait()
        {
            // false: ne pas retourner au contexte d'origine (meilleur pour librairies)
            await Task.Delay(1000).ConfigureAwait(false);
        }
    }

    // ============================================
    // GENERICS (TYPES GÉNÉRIQUES)
    // ============================================

    // Classe générique
    public class GenericBox<T>
    {
        private T _value;

        public void Set(T value)
        {
            _value = value;
        }

        public T Get()
        {
            return _value;
        }
    }

    // Classe générique avec contrainte
    public class GenericRepository<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public List<T> GetAll()
        {
            return _items;
        }
    }

    // Méthode générique
    public class GenericMethods
    {
        public T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    // ============================================
    // NULLABLE REFERENCE TYPES (C# 8.0+)
    // ============================================

    public class NullableReferenceTypes
    {
        // Type non-nullable
        public string Name { get; set; } = "";

        // Type nullable
        public string? NickName { get; set; }

        public void UseNullable()
        {
            string? maybeNull = GetNullableString();

            // Vérification null
            if (maybeNull != null)
            {
                Console.WriteLine(maybeNull.Length);
            }

            // Opérateur null-forgiving (!)
            string notNull = maybeNull!;

            // Null-coalescing
            string value = maybeNull ?? "Défaut";
        }

        public string? GetNullableString()
        {
            return null;
        }
    }

    // ============================================
    // PATTERN MATCHING
    // ============================================

    public class PatternMatching
    {
        public void TypePattern(object obj)
        {
            // Pattern matching avec is
            if (obj is string s)
            {
                Console.WriteLine($"String de longueur {s.Length}");
            }
            else if (obj is int i)
            {
                Console.WriteLine($"Entier: {i}");
            }
        }

        public string SwitchPattern(object obj)
        {
            return obj switch
            {
                string s => $"String: {s}",
                int i when i > 0 => "Entier positif",
                int i => "Entier négatif ou zéro",
                null => "Null",
                _ => "Autre type"
            };
        }

        public void PropertyPattern(Person person)
        {
            var message = person switch
            {
                { Age: < 13 } => "Enfant",
                { Age: >= 13 and < 18 } => "Adolescent",
                { Age: >= 18 } => "Adulte",
                _ => "Inconnu"
            };
        }
    }

    // ============================================
    // EXTENSION METHODS
    // ============================================

    public static class StringExtensions
    {
        // Méthode d'extension pour string
        public static bool IsValidEmail(this string email)
        {
            return Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }

        public static string Truncate(this string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= maxLength)
                return str;

            return str.Substring(0, maxLength) + "...";
        }

        public static int WordCount(this string str)
        {
            return str.Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

    public static class IntExtensions
    {
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static bool IsPrime(this int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }

    // ============================================
    // INDEXERS
    // ============================================

    public class CustomCollection
    {
        private string[] _items = new string[10];

        // Indexer
        public string this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        // Indexer avec string
        private Dictionary<string, int> _dict = new Dictionary<string, int>();

        public int this[string key]
        {
            get { return _dict.ContainsKey(key) ? _dict[key] : 0; }
            set { _dict[key] = value; }
        }
    }

    // ============================================
    // TYPES DE DONNÉES ET VARIABLES
    // ============================================

    public class VariableDeclarations
    {
        // Types numériques entiers
        byte byteVar = 255;                    // 0 à 255 (8 bits)
        sbyte sbyteVar = -128;                 // -128 à 127 (8 bits signé)
        short shortVar = -32768;               // -32,768 à 32,767 (16 bits)
        ushort ushortVar = 65535;              // 0 à 65,535 (16 bits non signé)
        int intVar = -2147483648;              // -2,147,483,648 à 2,147,483,647 (32 bits)
        uint uintVar = 4294967295;             // 0 à 4,294,967,295 (32 bits non signé)
        long longVar = -9223372036854775808;   // -9,223,372,036,854,775,808 à 9,223,372,036,854,775,807 (64 bits)
        ulong ulongVar = 18446744073709551615; // 0 à 18,446,744,073,709,551,615 (64 bits non signé)

        // Types numériques décimaux
        float floatVar = 3.14f;                // Précision ~7 chiffres (32 bits)
        double doubleVar = 3.14159265359;      // Précision ~15-16 chiffres (64 bits)
        decimal decimalVar = 3.141592653589793238m; // Précision ~28-29 chiffres (128 bits)

        // Types booléens et caractères
        bool boolVar = true;                   // true ou false
        char charVar = 'A';                    // Caractère Unicode unique

        // Types de référence
        string stringVar = "Hello World";      // Chaîne de caractères
        object objectVar = new object();       // Type de base pour tous les objets

        // Types nullables
        int? nullableInt = null;               // Peut contenir null
        double? nullableDouble = 3.14;

        // Inférence de type
        var inferredInt = 42;                  // Le compilateur déduit int
        var inferredString = "text";           // Le compilateur déduit string

        // Constantes
        const double PI = 3.14159;             // Ne peut pas être modifiée
        readonly int ReadOnlyField;            // Peut être assignée uniquement dans le constructeur

        public VariableDeclarations()
        {
            ReadOnlyField = 100;               // Initialisation dans constructeur
        }
    }

    // ============================================
    // OPÉRATEURS
    // ============================================

    public class Operators
    {
        public void ArithmeticOperators()
        {
            int a = 10, b = 3;
            int addition = a + b;              // 13
            int subtraction = a - b;           // 7
            int multiplication = a * b;        // 30
            int division = a / b;              // 3 (division entière)
            int modulo = a % b;                // 1 (reste de la division)

            // Incrémentation et décrémentation
            int x = 5;
            x++;                               // Post-incrémentation: x = 6
            ++x;                               // Pré-incrémentation: x = 7
            x--;                               // Post-décrémentation: x = 6
            --x;                               // Pré-décrémentation: x = 5
        }

        public void ComparisonOperators()
        {
            int a = 10, b = 20;
            bool equal = (a == b);             // false
            bool notEqual = (a != b);          // true
            bool greaterThan = (a > b);        // false
            bool lessThan = (a < b);           // true
            bool greaterOrEqual = (a >= b);    // false
            bool lessOrEqual = (a <= b);       // true
        }

        public void LogicalOperators()
        {
            bool a = true, b = false;
            bool and = a && b;                 // false (ET logique)
            bool or = a || b;                  // true (OU logique)
            bool not = !a;                     // false (NON logique)
        }

        public void AssignmentOperators()
        {
            int x = 10;
            x += 5;                            // x = x + 5 => 15
            x -= 3;                            // x = x - 3 => 12
            x *= 2;                            // x = x * 2 => 24
            x /= 4;                            // x = x / 4 => 6
            x %= 4;                            // x = x % 4 => 2
        }

        public void OtherOperators()
        {
            // Opérateur ternaire
            int age = 18;
            string status = (age >= 18) ? "Majeur" : "Mineur";

            // Opérateur null-coalescing
            string name = null;
            string displayName = name ?? "Anonyme"; // "Anonyme"

            // Opérateur null-conditional
            string upperName = name?.ToUpper();     // null (pas d'exception)

            // Opérateur de coalescence null avec assignation
            name ??= "Valeur par défaut";           // Assigne si null
        }
    }

    // ============================================
    // STRUCTURES DE CONTRÔLE - BOUCLES
    // ============================================

    public class Loops
    {
        public void ForLoop()
        {
            // Boucle for classique
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }

            // Boucle for avec plusieurs variables
            for (int i = 0, j = 10; i < j; i++, j--)
            {
                Console.WriteLine($"i: {i}, j: {j}");
            }
        }

        public void WhileLoop()
        {
            int counter = 0;
            while (counter < 5)
            {
                Console.WriteLine(counter);
                counter++;
            }
        }

        public void DoWhileLoop()
        {
            int counter = 0;
            do
            {
                Console.WriteLine(counter);
                counter++;
            } while (counter < 5);
        }

        public void ForEachLoop()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }

            // Avec liste
            List<string> names = new List<string> { "Alice", "Bob", "Charlie" };
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }

        public void LoopControlStatements()
        {
            // break: sort de la boucle
            for (int i = 0; i < 10; i++)
            {
                if (i == 5) break;
                Console.WriteLine(i);
            }

            // continue: passe à l'itération suivante
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0) continue;
                Console.WriteLine(i); // Affiche uniquement les nombres impairs
            }
        }
    }

    // ============================================
    // TABLEAUX (ARRAYS)
    // ============================================

    public class Arrays
    {
        public void OneDimensionalArrays()
        {
            // Déclaration et initialisation
            int[] numbers1 = new int[5];                    // Tableau de 5 éléments (initialisés à 0)
            int[] numbers2 = new int[] { 1, 2, 3, 4, 5 };  // Avec valeurs
            int[] numbers3 = { 1, 2, 3, 4, 5 };            // Forme courte

            // Accès aux éléments
            int first = numbers2[0];                        // 1
            numbers2[0] = 10;                               // Modification

            // Propriétés
            int length = numbers2.Length;                   // 5
        }

        public void MultiDimensionalArrays()
        {
            // Tableau 2D (matrice)
            int[,] matrix = new int[3, 4];                  // 3 lignes, 4 colonnes
            int[,] matrix2 = { { 1, 2 }, { 3, 4 }, { 5, 6 } };

            // Accès
            int element = matrix2[0, 1];                    // 2
            matrix2[1, 0] = 10;

            // Dimensions
            int rows = matrix2.GetLength(0);                // 3
            int cols = matrix2.GetLength(1);                // 2
        }

        public void JaggedArrays()
        {
            // Tableau de tableaux (tailles différentes)
            int[][] jagged = new int[3][];
            jagged[0] = new int[] { 1, 2 };
            jagged[1] = new int[] { 3, 4, 5 };
            jagged[2] = new int[] { 6, 7, 8, 9 };

            // Accès
            int element = jagged[1][2];                     // 5
        }

        public void ArrayMethods()
        {
            int[] numbers = { 5, 2, 8, 1, 9 };

            // Tri
            Array.Sort(numbers);                            // { 1, 2, 5, 8, 9 }

            // Inversion
            Array.Reverse(numbers);                         // { 9, 8, 5, 2, 1 }

            // Recherche
            int index = Array.IndexOf(numbers, 5);          // 2

            // Copie
            int[] copy = new int[5];
            Array.Copy(numbers, copy, 5);

            // Redimensionnement
            Array.Resize(ref numbers, 10);
        }
    }

    // ============================================
    // LISTES (LISTS)
    // ============================================

    public class Lists
    {
        public void BasicListOperations()
        {
            // Déclaration et initialisation
            List<int> numbers = new List<int>();
            List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5 };
            List<string> names = new List<string> { "Alice", "Bob", "Charlie" };

            // Ajout d'éléments
            numbers.Add(10);                                // Ajoute à la fin
            numbers.AddRange(new int[] { 20, 30, 40 });     // Ajoute plusieurs éléments
            numbers.Insert(0, 5);                           // Insère à l'index 0

            // Accès aux éléments
            int first = numbers[0];
            numbers[1] = 100;                               // Modification

            // Suppression
            numbers.Remove(10);                             // Supprime la première occurrence de 10
            numbers.RemoveAt(0);                            // Supprime à l'index 0
            numbers.RemoveRange(0, 2);                      // Supprime 2 éléments à partir de l'index 0
            numbers.Clear();                                // Supprime tous les éléments
        }

        public void ListMethods()
        {
            List<int> numbers = new List<int> { 5, 2, 8, 1, 9, 2 };

            // Recherche
            bool contains = numbers.Contains(5);            // true
            int index = numbers.IndexOf(2);                 // 1 (première occurrence)
            int lastIndex = numbers.LastIndexOf(2);         // 5 (dernière occurrence)
            int count = numbers.Count;                      // 6

            // Tri
            numbers.Sort();                                 // { 1, 2, 2, 5, 8, 9 }
            numbers.Reverse();                              // { 9, 8, 5, 2, 2, 1 }

            // Conversion
            int[] array = numbers.ToArray();

            // Recherche avec condition
            int firstEven = numbers.Find(x => x % 2 == 0);  // 8
            List<int> allEven = numbers.FindAll(x => x % 2 == 0); // { 8, 2, 2 }
            bool anyGreaterThan5 = numbers.Exists(x => x > 5);    // true
        }
    }

    // ============================================
    // DICTIONNAIRES ET COLLECTIONS
    // ============================================

    public class Collections
    {
        public void Dictionaries()
        {
            // Déclaration et initialisation
            Dictionary<string, int> ages = new Dictionary<string, int>();
            Dictionary<string, int> ages2 = new Dictionary<string, int>
            {
                { "Alice", 25 },
                { "Bob", 30 },
                { "Charlie", 35 }
            };

            // Ajout
            ages.Add("David", 40);
            ages["Eve"] = 28;                               // Ou modification si existe

            // Accès
            int aliceAge = ages2["Alice"];                  // 25

            // Vérification
            bool hasAlice = ages2.ContainsKey("Alice");     // true
            bool hasAge30 = ages2.ContainsValue(30);        // true

            // Suppression
            ages2.Remove("Bob");

            // Tentative d'accès sécurisé
            if (ages2.TryGetValue("Alice", out int age))
            {
                Console.WriteLine($"Alice a {age} ans");
            }

            // Parcours
            foreach (KeyValuePair<string, int> kvp in ages2)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Parcours des clés et valeurs séparément
            foreach (string key in ages2.Keys)
            {
                Console.WriteLine(key);
            }

            foreach (int value in ages2.Values)
            {
                Console.WriteLine(value);
            }
        }

        public void OtherCollections()
        {
            // HashSet (pas de doublons)
            HashSet<int> uniqueNumbers = new HashSet<int> { 1, 2, 3, 2, 1 }; // { 1, 2, 3 }
            uniqueNumbers.Add(4);

            // Queue (FIFO - First In First Out)
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("Premier");
            queue.Enqueue("Deuxième");
            string first = queue.Dequeue();                 // "Premier"
            string peek = queue.Peek();                     // "Deuxième" (sans retirer)

            // Stack (LIFO - Last In First Out)
            Stack<string> stack = new Stack<string>();
            stack.Push("Premier");
            stack.Push("Deuxième");
            string last = stack.Pop();                      // "Deuxième"
            string top = stack.Peek();                      // "Premier" (sans retirer)
        }
    }

    // ============================================
    // TUPLES
    // ============================================

    public class Tuples
    {
        public void BasicTuples()
        {
            // Tuple avec 2 éléments
            Tuple<int, string> tuple1 = new Tuple<int, string>(1, "One");
            int number = tuple1.Item1;                      // 1
            string text = tuple1.Item2;                     // "One"

            // Tuple avec 3 éléments
            Tuple<int, string, bool> tuple2 = Tuple.Create(42, "Answer", true);
        }

        public void ValueTuples()
        {
            // ValueTuple (plus moderne et performant)
            (int, string) tuple1 = (1, "One");
            int number = tuple1.Item1;
            string text = tuple1.Item2;

            // Avec noms
            (int Id, string Name, int Age) person = (1, "Alice", 25);
            Console.WriteLine($"{person.Name} a {person.Age} ans");

            // Déconstruction
            (int id, string name, int age) = person;
            Console.WriteLine($"{name} - {age}");

            // Déconstruction partielle
            (int id2, _, int age2) = person;                // Ignore le nom avec _
        }

        public (int Sum, int Product) CalculateTuple(int a, int b)
        {
            // Retourne un tuple
            return (a + b, a * b);
        }

        public void UseTupleReturn()
        {
            var result = CalculateTuple(5, 3);
            Console.WriteLine($"Somme: {result.Sum}, Produit: {result.Product}");

            // Ou avec déconstruction
            (int sum, int product) = CalculateTuple(5, 3);
        }
    }

    // ============================================
    // LECTURE ET MANIPULATION DE STRINGS
    // ============================================

    public class StringOperations
    {
        public void StringInput()
        {
            // Lecture depuis la console
            Console.Write("Entrez votre nom: ");
            string name = Console.ReadLine();

            // Lecture d'un caractère
            Console.Write("Appuyez sur une touche: ");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
        }

        public void StringManipulation()
        {
            string text = "  Hello World  ";

            // Longueur
            int length = text.Length;                       // 15

            // Nettoyage
            string trimmed = text.Trim();                   // "Hello World"
            string trimStart = text.TrimStart();            // "Hello World  "
            string trimEnd = text.TrimEnd();                // "  Hello World"

            // Casse
            string upper = text.ToUpper();                  // "  HELLO WORLD  "
            string lower = text.ToLower();                  // "  hello world  "

            // Remplacement
            string replaced = text.Replace("World", "C#");  // "  Hello C#  "

            // Substring
            string sub = "Hello World".Substring(0, 5);     // "Hello"
            string sub2 = "Hello World".Substring(6);       // "World"

            // Recherche
            bool contains = text.Contains("Hello");         // true
            bool startsWith = text.StartsWith("  He");      // true
            bool endsWith = text.EndsWith("ld  ");          // true
            int indexOf = text.IndexOf("World");            // 8

            // Division
            string[] words = "Hello,World,C#".Split(',');   // { "Hello", "World", "C#" }

            // Jointure
            string joined = string.Join("-", words);        // "Hello-World-C#"

            // Vérifications
            bool isEmpty = string.IsNullOrEmpty(text);      // false
            bool isWhiteSpace = string.IsNullOrWhiteSpace("   "); // true
        }

        public void StringFormatting()
        {
            string name = "Alice";
            int age = 25;

            // Concaténation
            string concat = "Je m'appelle " + name + " et j'ai " + age + " ans";

            // String.Format
            string formatted = string.Format("Je m'appelle {0} et j'ai {1} ans", name, age);

            // Interpolation (recommandé)
            string interpolated = $"Je m'appelle {name} et j'ai {age} ans";

            // Avec expressions
            string expr = $"Dans 5 ans, j'aurai {age + 5} ans";

            // Formatage de nombres
            double pi = 3.14159265359;
            string formatted2 = $"Pi = {pi:F2}";            // "Pi = 3.14" (2 décimales)
            string currency = $"{1234.56:C}";               // Format monétaire
            string percent = $"{0.85:P}";                   // "85.00%" (pourcentage)
        }

        public void StringBuilderExample()
        {
            // StringBuilder pour constructions de strings complexes (plus performant)
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("Hello");
            sb.Append(" ");
            sb.Append("World");
            sb.AppendLine("!");                             // Ajoute avec saut de ligne
            sb.Insert(0, ">>> ");                           // Insère au début
            string result = sb.ToString();                  // ">>> Hello World!\n"
        }
    }

    // ============================================
    // FONCTIONS (MÉTHODES)
    // ============================================

    public class Functions
    {
        // Fonction void (ne retourne rien)
        public void VoidMethod()
        {
            Console.WriteLine("Cette méthode ne retourne rien");
        }

        // Fonction avec retour
        public int Add(int a, int b)
        {
            return a + b;
        }

        // Fonction avec plusieurs paramètres
        public double Calculate(double x, double y, char operation)
        {
            switch (operation)
            {
                case '+': return x + y;
                case '-': return x - y;
                case '*': return x * y;
                case '/': return y != 0 ? x / y : 0;
                default: return 0;
            }
        }

        // Paramètres optionnels (valeurs par défaut)
        public void Greet(string name = "Invité", string salutation = "Bonjour")
        {
            Console.WriteLine($"{salutation}, {name}!");
        }

        // Paramètres nommés
        public void UseNamedParameters()
        {
            Greet(salutation: "Bonsoir", name: "Alice");
        }

        // Paramètres params (nombre variable d'arguments)
        public int Sum(params int[] numbers)
        {
            int total = 0;
            foreach (int num in numbers)
            {
                total += num;
            }
            return total;
        }

        public void UseParams()
        {
            int result1 = Sum(1, 2, 3);                     // 6
            int result2 = Sum(1, 2, 3, 4, 5);               // 15
        }

        // Paramètres ref (passage par référence - doit être initialisé)
        public void MultiplyByTwo(ref int number)
        {
            number *= 2;
        }

        public void UseRef()
        {
            int x = 5;
            MultiplyByTwo(ref x);                           // x vaut maintenant 10
        }

        // Paramètres out (retour multiple - pas besoin d'initialiser)
        public void GetMinMax(int[] numbers, out int min, out int max)
        {
            min = numbers.Min();
            max = numbers.Max();
        }

        public void UseOut()
        {
            int[] nums = { 1, 5, 3, 9, 2 };
            GetMinMax(nums, out int minimum, out int maximum);
            Console.WriteLine($"Min: {minimum}, Max: {maximum}");
        }

        // Expression-bodied members (syntaxe courte)
        public int Square(int x) => x * x;

        public string GetFullName(string first, string last) => $"{first} {last}";

        // Méthode statique
        public static double CalculateCircleArea(double radius)
        {
            return Math.PI * radius * radius;
        }

        // Méthode récursive
        public int Factorial(int n)
        {
            if (n <= 1) return 1;
            return n * Factorial(n - 1);
        }

        // Fonction locale (C# 7.0+)
        public int ComplexCalculation(int x, int y)
        {
            int LocalHelper(int a)
            {
                return a * 2;
            }

            return LocalHelper(x) + LocalHelper(y);
        }
    }

    // ============================================
    // TYPES DE RETOUR
    // ============================================

    public class ReturnTypes
    {
        // Retourne void (rien)
        public void NoReturn()
        {
            Console.WriteLine("Pas de retour");
            // return; est optionnel
        }

        // Retourne un type primitif
        public int ReturnInt() => 42;
        public double ReturnDouble() => 3.14;
        public bool ReturnBool() => true;
        public char ReturnChar() => 'A';
        public string ReturnString() => "Hello";

        // Retourne un objet
        public List<int> ReturnList()
        {
            return new List<int> { 1, 2, 3 };
        }

        public Dictionary<string, int> ReturnDictionary()
        {
            return new Dictionary<string, int>
            {
                { "A", 1 },
                { "B", 2 }
            };
        }

        // Retourne un tableau
        public int[] ReturnArray()
        {
            return new int[] { 1, 2, 3, 4, 5 };
        }

        // Retourne un tuple
        public (int, string) ReturnTuple()
        {
            return (1, "One");
        }

        public (int Id, string Name, int Age) ReturnNamedTuple()
        {
            return (1, "Alice", 25);
        }

        // Retourne un type nullable
        public int? ReturnNullableInt(bool returnNull)
        {
            return returnNull ? null : 42;
        }

        // Retourne un objet personnalisé
        public Person ReturnCustomObject()
        {
            return new Person { Name = "Alice", Age = 25 };
        }

        // Retourne une collection avec yield (génération paresseuse)
        public IEnumerable<int> GenerateNumbers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    // ============================================
    // STRUCTURES CONDITIONNELLES
    // ============================================

    public class Conditionals
    {
        public void IfElseStatements()
        {
            int age = 18;

            // If simple
            if (age >= 18)
            {
                Console.WriteLine("Majeur");
            }

            // If-else
            if (age >= 18)
            {
                Console.WriteLine("Majeur");
            }
            else
            {
                Console.WriteLine("Mineur");
            }

            // If-else if-else
            if (age < 13)
            {
                Console.WriteLine("Enfant");
            }
            else if (age < 18)
            {
                Console.WriteLine("Adolescent");
            }
            else if (age < 65)
            {
                Console.WriteLine("Adulte");
            }
            else
            {
                Console.WriteLine("Senior");
            }
        }

        public void SwitchStatements()
        {
            int dayNumber = 3;

            // Switch classique
            switch (dayNumber)
            {
                case 1:
                    Console.WriteLine("Lundi");
                    break;
                case 2:
                    Console.WriteLine("Mardi");
                    break;
                case 3:
                    Console.WriteLine("Mercredi");
                    break;
                default:
                    Console.WriteLine("Autre jour");
                    break;
            }

            // Switch avec plusieurs cas
            switch (dayNumber)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    Console.WriteLine("Jour de semaine");
                    break;
                case 6:
                case 7:
                    Console.WriteLine("Week-end");
                    break;
            }

            // Switch expression (C# 8.0+)
            string day = dayNumber switch
            {
                1 => "Lundi",
                2 => "Mardi",
                3 => "Mercredi",
                4 => "Jeudi",
                5 => "Vendredi",
                6 => "Samedi",
                7 => "Dimanche",
                _ => "Invalide"
            };
        }

        public void TernaryOperator()
        {
            int age = 20;

            // Opérateur ternaire
            string status = (age >= 18) ? "Majeur" : "Mineur";

            // Ternaire imbriqué
            string category = (age < 13) ? "Enfant" : (age < 18) ? "Adolescent" : "Adulte";
        }
    }

    // ============================================
    // GESTION D'ERREURS
    // ============================================

    public class ErrorHandling
    {
        public void TryCatchFinally()
        {
            try
            {
                int result = 10 / 0;                        // Exception DivideByZeroException
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur générale: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Toujours exécuté");
            }
        }

        public void ThrowException()
        {
            throw new ArgumentException("Valeur invalide");
        }

        public int ParseWithTry(string input)
        {
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            return 0;
        }
    }

    // ============================================
    // LINQ (Language Integrated Query)
    // ============================================

    public class LinqExamples
    {
        public void BasicLinq()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Where (filtre)
            var evenNumbers = numbers.Where(n => n % 2 == 0);           // { 2, 4, 6, 8, 10 }

            // Select (projection)
            var squares = numbers.Select(n => n * n);                   // { 1, 4, 9, 16, ... }

            // OrderBy (tri croissant)
            var ordered = numbers.OrderBy(n => n);

            // OrderByDescending (tri décroissant)
            var orderedDesc = numbers.OrderByDescending(n => n);

            // First (premier élément)
            int first = numbers.First();                                // 1
            int firstEven = numbers.First(n => n % 2 == 0);            // 2

            // FirstOrDefault (premier ou valeur par défaut)
            int firstOrDefault = numbers.FirstOrDefault(n => n > 100);  // 0

            // Any (au moins un élément correspond)
            bool hasEven = numbers.Any(n => n % 2 == 0);               // true

            // All (tous les éléments correspondent)
            bool allPositive = numbers.All(n => n > 0);                // true

            // Count (nombre d'éléments)
            int count = numbers.Count(n => n > 5);                      // 5

            // Sum, Average, Min, Max
            int sum = numbers.Sum();                                    // 55
            double average = numbers.Average();                         // 5.5
            int min = numbers.Min();                                    // 1
            int max = numbers.Max();                                    // 10

            // Take (prend les n premiers)
            var firstThree = numbers.Take(3);                           // { 1, 2, 3 }

            // Skip (saute les n premiers)
            var afterThree = numbers.Skip(3);                           // { 4, 5, 6, ... }

            // Distinct (éléments uniques)
            var unique = new List<int> { 1, 2, 2, 3, 3, 3 }.Distinct(); // { 1, 2, 3 }
        }
    }

    // ============================================
    // EXEMPLE COMPLET - PROGRAMME PRINCIPAL
    // ============================================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CHEAT SHEET C# COMPLÈTE ===\n");

            // Variables
            int age = 25;
            string name = "Alice";
            double height = 1.75;
            bool isStudent = true;

            // Affichage
            Console.WriteLine($"Nom: {name}, Age: {age}, Taille: {height}m, Étudiant: {isStudent}");

            // Tableau
            int[] numbers = { 1, 2, 3, 4, 5 };

            // Liste
            List<string> fruits = new List<string> { "Pomme", "Banane", "Orange" };

            // Dictionnaire
            Dictionary<string, int> scores = new Dictionary<string, int>
            {
                { "Alice", 95 },
                { "Bob", 87 },
                { "Charlie", 92
                using System;
            using System.Collections.Generic;
            using System.Linq;
            using System.IO;
            using System.Text.Json;
            using System.Xml.Serialization;

namespace CSharpCheatSheet
    {
        // ============================================
        // CONVERSIONS DE TYPES
        // ============================================

        public class TypeConversions
        {
            public void ImplicitConversions()
            {
                // Conversions implicites (automatiques, sans perte de données)
                int intValue = 100;
                long longValue = intValue;              // int -> long
                float floatValue = intValue;            // int -> float
                double doubleValue = intValue;          // int -> double
            }

            public void ExplicitConversions()
            {
                // Conversions explicites (cast, peut perdre des données)
                double doubleValue = 123.45;
                int intValue = (int)doubleValue;        // 123 (perd la partie décimale)

                long longValue = 1000L;
                int intValue2 = (int)longValue;
            }

            public void ParseMethods()
            {
                // Parse (lève une exception si échec)
                int number = int.Parse("123");
                double pi = double.Parse("3.14");
                bool flag = bool.Parse("true");
                DateTime date = DateTime.Parse("2024-01-01");

                // TryParse (retourne false si échec, plus sûr)
                if (int.TryParse("123", out int result))
                {
                    Console.WriteLine($"Converti: {result}");
                }

                if (double.TryParse("3.14", out double piValue))
                {
                    Console.WriteLine($"Pi: {piValue}");
                }
            }

            public void ConvertClass()
            {
                // Utilisation de la classe Convert
                string numStr = "123";
                int num = Convert.ToInt32(numStr);
                double dbl = Convert.ToDouble("3.14");
                bool flag = Convert.ToBoolean(1);       // 1 -> true, 0 -> false
                string str = Convert.ToString(123);

                // Convert gère null (retourne 0 ou valeur par défaut)
                string nullStr = null;
                int zero = Convert.ToInt32(nullStr);    // 0 (Parse lancerait une exception)
            }

            public void ToStringMethod()
            {
                // ToString() disponible sur tous les types
                int number = 123;
                string str1 = number.ToString();        // "123"

                double pi = 3.14159;
                string str2 = pi.ToString("F2");        // "3.14" (2 décimales)

                DateTime now = DateTime.Now;
                string str3 = now.ToString("dd/MM/yyyy"); // "05/10/2025"
            }
        }

        // ============================================
        // DATES ET HEURES
        // ============================================

        public class DateTimeExamples
        {
            public void BasicDateTime()
            {
                // DateTime actuel
                DateTime now = DateTime.Now;                    // Date et heure actuelles
                DateTime today = DateTime.Today;                // Date sans heure (00:00:00)
                DateTime utcNow = DateTime.UtcNow;             // Heure UTC

                // Créer une DateTime spécifique
                DateTime specific = new DateTime(2024, 12, 25); // 25 décembre 2024
                DateTime withTime = new DateTime(2024, 12, 25, 15, 30, 0); // Avec heure

                // Propriétés
                int year = now.Year;                            // 2025
                int month = now.Month;                          // 10
                int day = now.Day;                              // 5
                int hour = now.Hour;                            // Heure actuelle
                int minute = now.Minute;
                int second = now.Second;
                DayOfWeek dayOfWeek = now.DayOfWeek;           // Sunday, Monday, etc.
            }

            public void DateTimeOperations()
            {
                DateTime date = new DateTime(2024, 1, 1);

                // Ajouter/Soustraire
                DateTime tomorrow = date.AddDays(1);            // 2 janvier 2024
                DateTime nextMonth = date.AddMonths(1);         // 1 février 2024
                DateTime nextYear = date.AddYears(1);           // 1 janvier 2025
                DateTime later = date.AddHours(5);              // +5 heures
                DateTime earlier = date.AddDays(-7);            // -7 jours

                // Différence entre deux dates (TimeSpan)
                DateTime date1 = new DateTime(2024, 1, 1);
                DateTime date2 = new DateTime(2024, 12, 31);
                TimeSpan difference = date2 - date1;
                int days = difference.Days;                     // 365
                double totalHours = difference.TotalHours;
            }

            public void DateTimeFormatting()
            {
                DateTime now = DateTime.Now;

                // Formats standards
                string s1 = now.ToString("d");                  // Date courte: 05/10/2025
                string s2 = now.ToString("D");                  // Date longue: dimanche 5 octobre 2025
                string s3 = now.ToString("t");                  // Heure courte: 14:30
                string s4 = now.ToString("T");                  // Heure longue: 14:30:45
                string s5 = now.ToString("f");                  // Date et heure courtes
                string s6 = now.ToString("F");                  // Date et heure longues

                // Formats personnalisés
                string custom1 = now.ToString("dd/MM/yyyy");    // 05/10/2025
                string custom2 = now.ToString("yyyy-MM-dd");    // 2025-10-05
                string custom3 = now.ToString("HH:mm:ss");      // 14:30:45
                string custom4 = now.ToString("dddd dd MMMM yyyy"); // dimanche 05 octobre 2025
            }

            public void TimeSpanExamples()
            {
                // Créer un TimeSpan
                TimeSpan ts1 = new TimeSpan(1, 30, 0);          // 1h 30min
                TimeSpan ts2 = TimeSpan.FromHours(2.5);         // 2h 30min
                TimeSpan ts3 = TimeSpan.FromDays(7);            // 7 jours
                TimeSpan ts4 = TimeSpan.FromMinutes(90);        // 90 minutes

                // Propriétés
                int hours = ts1.Hours;                          // 1
                int minutes = ts1.Minutes;                      // 30
                double totalHours = ts1.TotalHours;             // 1.5
                double totalMinutes = ts1.TotalMinutes;         // 90

                // Opérations
                TimeSpan sum = ts1 + ts2;                       // Addition
                TimeSpan diff = ts2 - ts1;                      // Soustraction
            }
        }

        // ============================================
        // MATHÉMATIQUES
        // ============================================

        public class MathExamples
        {
            public void BasicMath()
            {
                // Constantes
                double pi = Math.PI;                            // 3.14159...
                double e = Math.E;                              // 2.71828...

                // Valeur absolue
                int abs1 = Math.Abs(-10);                       // 10
                double abs2 = Math.Abs(-3.14);                  // 3.14

                // Puissance et racine
                double power = Math.Pow(2, 3);                  // 8 (2^3)
                double sqrt = Math.Sqrt(16);                    // 4
                double cbrt = Math.Cbrt(27);                    // 3 (racine cubique)

                // Arrondi
                double round1 = Math.Round(3.5);                // 4
                double round2 = Math.Round(3.14159, 2);         // 3.14
                double ceiling = Math.Ceiling(3.1);             // 4
                double floor = Math.Floor(3.9);                 // 3
                double truncate = Math.Truncate(3.9);           // 3

                // Min/Max
                int min = Math.Min(5, 10);                      // 5
                int max = Math.Max(5, 10);                      // 10

                // Signe
                int sign1 = Math.Sign(5);                       // 1 (positif)
                int sign2 = Math.Sign(-5);                      // -1 (négatif)
                int sign3 = Math.Sign(0);                       // 0
            }

            public void TrigonometryAndLogarithms()
            {
                // Trigonométrie (angles en radians)
                double sin = Math.Sin(Math.PI / 2);             // 1
                double cos = Math.Cos(0);                       // 1
                double tan = Math.Tan(Math.PI / 4);             // 1

                // Conversion degrés <-> radians
                double radians = 90 * Math.PI / 180;            // 90° en radians
                double degrees = Math.PI / 2 * 180 / Math.PI;   // π/2 en degrés

                // Logarithmes
                double log = Math.Log(10);                      // Logarithme naturel
                double log10 = Math.Log10(100);                 // 2 (log base 10)
                double exp = Math.Exp(1);                       // e^1 = e
            }

            public void RandomNumbers()
            {
                Random random = new Random();

                // Nombre entier aléatoire
                int rand1 = random.Next();                      // 0 à int.MaxValue
                int rand2 = random.Next(10);                    // 0 à 9
                int rand3 = random.Next(1, 7);                  // 1 à 6 (dé)

                // Nombre décimal aléatoire
                double randDouble = random.NextDouble();        // 0.0 à 1.0

                // Tableau d'octets aléatoires
                byte[] buffer = new byte[10];
                random.NextBytes(buffer);
            }
        }

        // ============================================
        // SÉRIALISATION JSON
        // ============================================

        public class JsonSerialization
        {
            public class Product
            {
                public int Id { get; set; }
                public string Name { get; set; }
                public decimal Price { get; set; }
            }

            public void SerializeToJson()
            {
                // Créer un objet
                Product product = new Product
                {
                    Id = 1,
                    Name = "Ordinateur",
                    Price = 999.99m
                };

                // Sérialiser en JSON
                string json = JsonSerializer.Serialize(product);
                Console.WriteLine(json);
                // {"Id":1,"Name":"Ordinateur","Price":999.99}

                // Avec formatage (pretty print)
                var options = new JsonSerializerOptions { WriteIndented = true };
                string prettyJson = JsonSerializer.Serialize(product, options);
            }

            public void DeserializeFromJson()
            {
                string json = "{\"Id\":1,\"Name\":\"Ordinateur\",\"Price\":999.99}";

                // Désérialiser depuis JSON
                Product product = JsonSerializer.Deserialize<Product>(json);
                Console.WriteLine($"Nom: {product.Name}, Prix: {product.Price}");
            }

            public void WorkWithJsonFile()
            {
                Product product = new Product { Id = 1, Name = "Laptop", Price = 1200 };

                // Écrire dans un fichier JSON
                string json = JsonSerializer.Serialize(product);
                File.WriteAllText("product.json", json);

                // Lire depuis un fichier JSON
                string fileContent = File.ReadAllText("product.json");
                Product loadedProduct = JsonSerializer.Deserialize<Product>(fileContent);
            }
        }

        // ============================================
        // OPÉRATEURS BINAIRES (BITWISE)
        // ============================================

        public class BitwiseOperators
        {
            public void BasicBitwise()
            {
                int a = 5;  // 0101 en binaire
                int b = 3;  // 0011 en binaire

                // ET binaire (&)
                int and = a & b;                                // 0001 = 1

                // OU binaire (|)
                int or = a | b;                                 // 0111 = 7

                // XOR binaire (^)
                int xor = a ^ b;                                // 0110 = 6

                // Complément binaire (~)
                int not = ~a;                                   // Inverse tous les bits

                // Décalage à gauche (<<)
                int leftShift = a << 1;                         // 1010 = 10 (multiplie par 2)

                // Décalage à droite (>>)
                int rightShift = a >> 1;                        // 0010 = 2 (divise par 2)
            }

            public void BitwiseTricks()
            {
                // Vérifier si un nombre est pair
                int num = 42;
                bool isEven = (num & 1) == 0;                   // true

                // Multiplier par 2
                int doubled = num << 1;                         // 84

                // Diviser par 2
                int halved = num >> 1;                          // 21

                // Échanger deux variables sans variable temporaire
                int x = 5, y = 10;
                x = x ^ y;
                y = x ^ y;
                x = x ^ y;
                // Maintenant x = 10, y = 5
            }
        }

        // ============================================
        // ATTRIBUTS (ATTRIBUTES)
        // ============================================

        public class AttributeExamples
        {
            // Attribut Obsolete
            [Obsolete("Utilisez NewMethod() à la place")]
            public void OldMethod()
            {
                Console.WriteLine("Ancienne méthode");
            }

            public void NewMethod()
            {
                Console.WriteLine("Nouvelle méthode");
            }

            // Attribut personnalisé
            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
            public class AuthorAttribute : Attribute
            {
                public string Name { get; set; }
                public string Date { get; set; }

                public AuthorAttribute(string name)
                {
                    Name = name;
                    Date = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

            [Author("Alice")]
            public class MyClass
            {
                [Author("Bob")]
                public void MyMethod() { }
            }
        }

        // ============================================
        // YIELD ET ITÉRATEURS
        // ============================================

        public class YieldExamples
        {
            // Générateur simple
            public IEnumerable<int> GetNumbers()
            {
                yield return 1;
                yield return 2;
                yield return 3;
            }

            // Générateur avec boucle
            public IEnumerable<int> GetRange(int start, int count)
            {
                for (int i = 0; i < count; i++)
                {
                    yield return start + i;
                }
            }

            // Générateur infini (Fibonacci)
            public IEnumerable<int> Fibonacci()
            {
                int a = 0, b = 1;
                while (true)
                {
                    yield return a;
                    int temp = a;
                    a = b;
                    b = temp + b;
                }
            }

            public void UseYield()
            {
                // Utilisation normale
                foreach (int num in GetNumbers())
                {
                    Console.WriteLine(num);
                }

                // Fibonacci limité
                foreach (int fib in Fibonacci().Take(10))
                {
                    Console.WriteLine(fib);
                }
            }
        }

        // ============================================
        // OPÉRATEURS SURCHARGÉS (OPERATOR OVERLOADING)
        // ============================================

        public class Vector2D
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector2D(double x, double y)
            {
                X = x;
                Y = y;
            }

            // Surcharge de l'opérateur +
            public static Vector2D operator +(Vector2D v1, Vector2D v2)
            {
                return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
            }

            // Surcharge de l'opérateur -
            public static Vector2D operator -(Vector2D v1, Vector2D v2)
            {
                return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
            }

            // Surcharge de l'opérateur *
            public static Vector2D operator *(Vector2D v, double scalar)
            {
                return new Vector2D(v.X * scalar, v.Y * scalar);
            }

            // Surcharge de l'opérateur ==
            public static bool operator ==(Vector2D v1, Vector2D v2)
            {
                return v1.X == v2.X && v1.Y == v2.Y;
            }

            public static bool operator !=(Vector2D v1, Vector2D v2)
            {
                return !(v1 == v2);
            }

            public override bool Equals(object obj)
            {
                if (obj is Vector2D v)
                    return this == v;
                return false;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }

        // ============================================
        // MOTS-CLÉS UTILES
        // ============================================

        public class UsefulKeywords
        {
            // using pour IDisposable (libération automatique)
            public void UsingStatement()
            {
                using (StreamReader reader = new StreamReader("file.txt"))
                {
                    string content = reader.ReadToEnd();
                } // reader.Dispose() appelé automatiquement

                // Syntaxe simplifiée (C# 8.0+)
                using StreamReader reader2 = new StreamReader("file.txt");
                // Dispose appelé à la fin du scope
            }

            // lock pour synchronisation de threads
            private readonly object _lock = new object();
            private int _counter = 0;

            public void IncrementCounter()
            {
                lock (_lock)
                {
                    _counter++;
                }
            }

            // checked/unchecked pour overflow
            public void CheckedUnchecked()
            {
                // checked: lève OverflowException si dépassement
                checked
                {
                    int max = int.MaxValue;
                    // int overflow = max + 1; // OverflowException
                }

                // unchecked: ignore le dépassement (comportement par défaut)
                unchecked
                {
                    int max = int.MaxValue;
                    int overflow = max + 1;                     // -2147483648 (wraparound)
                }
            }

            // nameof pour obtenir le nom d'une variable/méthode
            public void UseNameOf()
            {
                int myVariable = 10;
                Console.WriteLine(nameof(myVariable));          // "myVariable"
                Console.WriteLine(nameof(UseNameOf));           // "UseNameOf"
            }

            // default pour valeur par défaut d'un type
            public void UseDefault()
            {
                int defaultInt = default(int);                  // 0
                string defaultString = default(string);         // null
                bool defaultBool = default;                     // false
            }

            // sizeof pour taille en octets
            public void UseSizeOf()
            {
                int sizeInt = sizeof(int);                      // 4
                int sizeDouble = sizeof(double);                // 8
                int sizeBool = sizeof(bool);                    // 1
            }
        }

        // ============================================
        // FONCTIONS MATHÉMATIQUES UTILES
        // ============================================

        public static class MathHelper
        {
            // Plus grand commun diviseur (PGCD)
            public static int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int temp = b;
                    b = a % b;
                    a = temp;
                }
                return a;
            }

            // Plus petit commun multiple (PPCM)
            public static int LCM(int a, int b)
            {
                return (a * b) / GCD(a, b);
            }

            // Vérifier si un nombre est premier
            public static bool IsPrime(int n)
            {
                if (n <= 1) return false;
                if (n == 2) return true;
                if (n % 2 == 0) return false;

                for (int i = 3; i <= Math.Sqrt(n); i += 2)
                {
                    if (n % i == 0) return false;
                }
                return true;
            }

            // Factorielle
            public static long Factorial(int n)
            {
                if (n <= 1) return 1;
                long result = 1;
                for (int i = 2; i <= n; i++)
                {
                    result *= i;
                }
                return result;
            }

            // Suite de Fibonacci
            public static int Fibonacci(int n)
            {
                if (n <= 1) return n;
                int a = 0, b = 1;
                for (int i = 2; i <= n; i++)
                {
                    int temp = a + b;
                    a = b;
                    b = temp;
                }
                return b;
            }

            // Puissance entière rapide
            public static long Power(int baseNum, int exponent)
            {
                long result = 1;
                while (exponent > 0)
                {
                    if ((exponent & 1) == 1)
                        result *= baseNum;
                    baseNum *= baseNum;
                    exponent >>= 1;
                }
                return result;
            }
        }

        // ============================================
        // RÉSUMÉ DES CONCEPTS CLÉS
        // ============================================

        /*
         * AVEC CETTE CHEAT SHEET, VOUS POUVEZ CODER EN C# :
         * 
         * ✅ Variables et types (primitifs, nullable, var, const, readonly)
         * ✅ Opérateurs (arithmétiques, logiques, binaires, ternaire, null-coalescing)
         * ✅ Structures de contrôle (if, switch, for, while, foreach)
         * ✅ Tableaux (1D, 2D, jagged)
         * ✅ Collections (List, Dictionary, HashSet, Queue, Stack)
         * ✅ Tuples (Tuple et ValueTuple)
         * ✅ Strings (manipulation, formatage, StringBuilder, Regex)
         * ✅ Fonctions (params, ref, out, optionnels, lambda, locales)
         * ✅ Classes (public, private, static, abstract, sealed)
         * ✅ Propriétés (auto, readonly, init, calculées)
         * ✅ Constructeurs (défaut, paramètres, chaînage, statique)
         * ✅ Héritage (virtual, override, base, abstract)
         * ✅ Interfaces (implémentation multiple)
         * ✅ Enums (simple, valeurs personnalisées, Flags)
         * ✅ Structs et Records
         * ✅ Generics (classes, méthodes, contraintes)
         * ✅ Delegates et Events
         * ✅ LINQ (Where, Select, OrderBy, First, Any, All, etc.)
         * ✅ Async/Await (Task, Task.WhenAll, ConfigureAwait)
         * ✅ Fichiers I/O (File, Directory, Path, StreamReader/Writer)
         * ✅ Regex (expressions régulières)
         * ✅ JSON (sérialisation/désérialisation)
         * ✅ DateTime et TimeSpan
         * ✅ Mathématiques (Math class, Random)
         * ✅ Gestion d'erreurs (try/catch/finally, throw)
         * ✅ Pattern Matching
         * ✅ Extension Methods
         * ✅ Indexers
         * ✅ Nullable Reference Types
         * ✅ Yield et Itérateurs
         * ✅ Operator Overloading
         * ✅ Attributs
         * ✅ Mots-clés utiles (using, lock, checked, nameof, default, sizeof)
         * 
         * CETTE CHEAT SHEET COUVRE 99% DES BESOINS EN C# !
         */
    }