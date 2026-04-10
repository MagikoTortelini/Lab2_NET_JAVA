Zadaniem laboratorium było stworzenie programu obsługującego zapytania API z wykorzystaniem parametrów rozszerzenie go o prostą bazę danych przechowującą dane z zapytania oraz zapoznanie się i stworzenie prostego UI za pomocą MAUI

Do laboratorium wykorzystano API kursów walut openexchangerates

Do działania API potrzebne jest wprowadzenie klucza API w klasie APITest oraz APITest_with_db w zmiennej API_KEY

Program składka się z kilku głównych klas  
*  rates_from_api - Klasa ta zajmuje się przechowaniem danych po deserializacji zapytania z API za pomocą przeciążenia funkcji ToString możemy wyświetlić z kiedy jest kurs oraz ile on wynosi  
*  APITest - Klasa ta zajmuje się obsługą zapytania API w części konsolowej funkcji GetData przyjmuje dwa parametry walutę z której przeliczamy kurs oraz walutę na którą przeliczamy kurs wynikiem  jest pobranie danych z API i wyświetlenie kursu  
*  APITest_with_db - Klasa ta zajmuje się obsługą API podczas wykorzystania bazy danych funkcji GetData dwa parametry walutę z której przeliczamy kurs oraz walutę na którą przeliczamy kurs.Funkcja początkowo pobiera wszystkie zapisane waluty w bazie danych i sprawdza czy któraś podanych w parametrach walut nie znajduje się w niej, gdy brakuje jednej z walut funkcja wykonuje zapytanie API i dodaje brakujące waluty do bazy następnie dodawany jest kurs tych walut do tablicy Exchanges_rates. W przypadku gdy obie waluty są w bazie program sprawdza czy istnieje zapisany między nimi kurs jeżeli go nie ma dodawany jest nowy kurs do bazy danych. Jeżeli istnieją waluty i kurs między nimi funkcja zwróci kurs, funkcja również aktualizuje kurs gdy data z której jest zapisany jest inna niż aktualna  
*  DB_operations - Klasa ta przechowuje funkcja używane w GUI do obsługi bazy posiada funkcje Add pozwalająca wykonująca operację klasy
  APITest_with_db(zwrócenie kursu podanego przez użytkowniku, w przypadku braku dodanie go i zwrócenie), show_sorted_down zwracającą waluty posortowane w odwrotnej alfabetycznej kolejności, show_sorted_up zwracającą waluty posortowane w alfabetycznej kolejności, show_filtered zwracające kursy waluty podanej przez użytkowania z bazy, show_currencies i show_rate zwracają wszystkie pozycje z tabel walut i kursów oraz funkcja add_to_db dodająca walutę i kurs podany ręcznie przez użytkownika  

Baza danych składa się z dwóch tablic Currencies oraz Exchanges_rates w relacji jeden do wielu (jedna waluta może mieć kilka kursów), tablica Currencies składa się z ID, kodu waluty, oraz jej przelicznikiem w USD(jest to bazowy kurs API), tablica Exchanges_rates składka się ID, ID waluty z której obliczamy, ID waluty na którą przeliczamy, kurs, data z której jest kurs 

Interfejs graficzny pól umożliwiających odczyt lub pobranie kursu walut, pól umożliwiających dodanie ręcznego wpisu do bazy walut, oraz przycisków odpowiedzialnych za pokazanie zawartości bazy, sortowanie oraz filtrację
