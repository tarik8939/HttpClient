using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfClient.Models;

namespace WpfClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const string APP_PATH = "https://localhost:44377";
        private Book selectedBook;

        private Command takeCommand;
        private Command giveCommand;

        public ObservableCollection<Book> Books { get; set; }

        public MainViewModel()
        {
            var db = new Context();
            db.Books.Load();
            Books = new ObservableCollection<Book>(LoadBooks());
        }
        private List<Book> LoadBooks()
        {
            var t = Task.Run(() => Load());
            t.Wait();
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(t.Result);
            return books;
        }

        private static async Task<String> Load()
        {
            using (var client = new HttpClient())
            {
                var response = string.Empty;
                HttpResponseMessage result = await client.GetAsync(APP_PATH + "/api/Books");
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
                return response;
            }

        }



        public Command TakeCommand
        {
            get
            {
                return takeCommand ??
                    (takeCommand = new Command(obj => {
                        Book book = obj as Book;
                        using (var client = new HttpClient())
                        {
                            var id = book.BookID;
                            var response = client.PutAsync(APP_PATH + $"/api/Books/TakeBook/{id}", null).Result;
                            var rez = response.StatusCode.ToString();

                            if(rez != "BadRequest")
                            {
                                var b = Books.First(x => x.BookID == book.BookID);
                                b.BookCount--;
                            }

                        }
                    }));
            }
        }

        public Command GiveCommand
        {
            get
            {
                return giveCommand ??
                    (giveCommand = new Command(obj => {
                        Book book = obj as Book;
                        using (var client = new HttpClient())
                        {

                            var id = book.BookID;
                            var response = client.PutAsync(APP_PATH + $"/api/Books/GiveBook/{id}", null).Result;
                            var rez = response.StatusCode.ToString();
                            var b = Books.First(x => x.BookID == book.BookID);
                            b.BookCount++;

                        }
                    }));
            }
        }





        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }
    }
}
