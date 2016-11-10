//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Windows.Storage;
//using Windows.Storage.Streams;

//namespace FootballClient.DataAccess.Providers
//{
//    public class ImagesProvider
//    {
//        private readonly TaskCompletionSource<bool> loadImagesTaskCompletionSource;
//        private const string PathImagePattern = "ms-appdata:///local/{0}/{1}";
//        private const string ImagesFolderKey = "AlbumCoversFolder";
//        private readonly bool isInitialized;

//        private readonly ConcurrentDictionary<string, TaskCompletionSource<StorageFile>> downloadedCompletionSources = new ConcurrentDictionary<string, TaskCompletionSource<StorageFile>>();

//        private static readonly Lazy<ImagesProvider> instanceLazy = new Lazy<ImagesProvider>(() => new ImagesProvider(), true);
//        private StorageFolder albumCoversFolder;

//        public static ImagesProvider Instance
//        {
//            get { return instanceLazy.Value; }
//        }

//        private ImagesProvider()
//        {
//            if (!isInitialized)
//            {
//                this.loadImagesTaskCompletionSource = new TaskCompletionSource<bool>();
//                this.LoadCachedImages();
//                isInitialized = true;
//            }
//        }

//        private void LoadCachedImages()
//        {
//            Task.Run(async () =>
//            {
//                albumCoversFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(ImagesFolderKey, CreationCollisionOption.OpenIfExists);
//                var albumCoversFiles = await albumCoversFolder.GetFilesAsync();
//                this.LoadImages(albumCoversFiles, ImagesFolderKey);
//                this.loadImagesTaskCompletionSource.SetResult(true);
//            });
//        }

//        private void LoadImages(IReadOnlyList<StorageFile> files, string folderName)
//        {
//            if (files != null && files.Any())
//            {
//                foreach (var storageFile in files)
//                {
//                    var imageTask = new TaskCompletionSource<StorageFile>();
//                    imageTask.TrySetResult(storageFile);
//                    downloadedCompletionSources.TryAdd(storageFile.Name, imageTask);
//                }
//            }
//        }

//        public async Task<StorageFile> LoadImageAsync(string imageUrl)
//        {
//            await loadImagesTaskCompletionSource.Task;
//            TaskCompletionSource<StorageFile> imageTask;
//            var imageFileName = this.PrepareFileName(imageUrl);
//            if (downloadedCompletionSources.Keys.Contains(imageFileName))
//            {
//                imageTask = downloadedCompletionSources[imageFileName];
//            }
//            else
//            {
//                imageTask = new TaskCompletionSource<StorageFile>();
//                var imageResult = await this.CacheImagesFromUri(imageFileName, imageUrl, ImagesFolderKey, imageTask);
//                downloadedCompletionSources.TryAdd(imageFileName, imageTask);
//            }

//            return await imageTask.Task;
//        }

//        internal string PrepareFileName(string value)
//        {
//            var fileName = value;
//            var chars = new List<char>(Path.GetInvalidFileNameChars())
//            {
//                '#'
//            };
//            foreach (var ch in chars.Where(ch => fileName.Contains(ch.ToString())))
//            {
//                fileName = fileName.Replace(ch.ToString(), string.Empty);
//            }

//            fileName = fileName.Trim();
//            return fileName;
//        }

//        private async Task<string> CacheImagesFromUri(string fileName, string sourceUrl, string folderName, TaskCompletionSource<StorageFile> storageFileTCS)
//        {
//            var url = string.Empty;

//            try
//            {
//                var sourceUri = new Uri(sourceUrl);

//#if DEBUG
//                var stopwatch = new Stopwatch();
//                stopwatch.Start();
//#endif
//                var randomAccessStreamReference = RandomAccessStreamReference.CreateFromUri(sourceUri);
//                using (var imageStream = await randomAccessStreamReference.OpenReadAsync())
//                {
//                    var storageFile = await CacheImageToFile(imageStream.AsStream(), fileName);
//                    storageFileTCS.TrySetResult(storageFile);
//                }
//#if DEBUG
//                stopwatch.Stop();
//                Debug.WriteLine("SaveImageToStorage: {0}", stopwatch.Elapsed.TotalMilliseconds);
//#endif

//                url = string.Format(PathImagePattern, folderName, fileName);
//            }
//            catch (Exception ex)
//            {
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//                //throw new Exception(ex.Message);
//#endif
//            }

//            return url;
//        }

//        private async Task<StorageFile> CacheImageToFile(Stream imageStream, string fileName)
//        {
//            var storageFile = await albumCoversFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
//            using (var fileStream = await storageFile.OpenStreamForWriteAsync())
//            {
//                await imageStream.CopyToAsync(fileStream);
//                await imageStream.FlushAsync();
//            }

//            return storageFile;
//        }
//    }
//}