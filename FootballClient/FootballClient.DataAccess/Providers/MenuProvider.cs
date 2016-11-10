//using FootballClient.DataAccess.Request;
//using FootballClient.DataAccess.Request.Parsers;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;
//using Windows.ApplicationModel;

//namespace FootballClient.DataAccess.Providers
//{
//    public class MenuProvider
//    {
//        private TaskCompletionSource<Menu> _menuCompletionSource;
//        public MenuProvider()
//        {
//            _menuCompletionSource = new TaskCompletionSource<Menu>();
//            Initialize();
//        }

//        public async Task<Menu> GetMenuAsync()
//        {
//            return await _menuCompletionSource.Task;
//        }

//        private async void Initialize()
//        {
//            Menu resultMenu = null;
//            var serverMenuResponse = await LoadMenuAsync();
//            if (serverMenuResponse.IsSuccess && serverMenuResponse.Result.Items != null
//                && serverMenuResponse.Result.Items.Length > 0)
//            {
//                resultMenu = serverMenuResponse.Result;
//            }
//            else
//            {
//                resultMenu = await LoadMockMenuAsync();
//            }

//            _menuCompletionSource.TrySetResult(resultMenu);
//        }

//        private async Task<Response<Menu>> LoadMenuAsync()
//        {
//            var request = new Request<Menu>();
//            request.ResponseParser = new XmlParser<Menu>();
//            request.RequestAddress = "http://football.ua/hnd/Android/Menu.ashx";

//            return await request.SendAsync(RequestAccessMode.Server, "AndroidMenu");
//        }

//        private async Task<Menu> LoadMockMenuAsync()
//        {
//            Menu menu = null;
//            var mockFile = await Package.Current.InstalledLocation.GetFileAsync("MenuMock.xml");
//            using (var fileStream = await mockFile.OpenReadAsync())
//            {
//                using (var stream = fileStream.AsStream())
//                {
//                    XmlSerializer serializer = new XmlSerializer(typeof(Menu));
//                    menu = (Menu)serializer.Deserialize(stream);
//                }
//            }

//            return menu;
//        }
//    }


//    [XmlType("menu")]
//    [XmlRoot(Namespace = "", IsNullable = false)]
//    public class Menu
//    {
//        [XmlElement("item")]
//        public MenuItem[] Items { get; set; }
//    }

//    [XmlType(TypeName = "menuItem", AnonymousType = true)]
//    public class MenuItem
//    {
//        [XmlElement("sub")]
//        public MenuItemSub[] Subs { get; set; }

//        [XmlAttribute("name")]
//        public string Name { get; set; }
//    }

//    [XmlType(TypeName = "menuItemSub", AnonymousType = true)]
//    public class MenuItemSub
//    {
//        [XmlAttribute("name")]
//        public string Name { get; set; }

//        [XmlAttribute("type")]
//        public string Type { get; set; }

//        [XmlAttribute("url")]
//        public string Url { get; set; }
//    }
//}
