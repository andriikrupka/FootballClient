//using Windows.ApplicationModel.Background;

//namespace FootballClient.DataAccess.Providers
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using System.Threading.Tasks;

//    public enum BackgroundTaskRegistrationResult
//    {
//        Registered,

//        NotRegistered,

//        AlreadyRegistered
//    }
    
//    public class SettingsProvider
//    {
//        private const string UpdateTaskName = "UpdateMainTileBackgroundTask";

//        public async Task RegisterBackgroundTaskAsync()
//        {
//            var isRegistered = this.IsBackgroundTaskRegistered();
//            if (!isRegistered)
//            {
//                var builder = new BackgroundTaskBuilder();
//                var result = await BackgroundExecutionManager.RequestAccessAsync();
//                if (result != BackgroundAccessStatus.Denied)
//                {
//                    builder.Name = UpdateTaskName;
//                    builder.TaskEntryPoint = "FootballClient.BackgroundTask.UpdateTileBackgroundTask";
//                    var timeTrigger = new TimeTrigger(15, false);
//                    builder.SetTrigger(timeTrigger);
//                    builder.Register();
//                    //await DataProviders.TileProvider.UpdateMainTile();
//                }
//            }
//        }

//        public void UnregisterBackgroundTask()
//        {
//            var task = BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name == UpdateTaskName);
//            if (task.Key != Guid.Empty)
//            {
//                task.Value.Unregister(true);
//                //DataProviders.TileProvider.ClearMainTile();
//            }
//        }

//        public bool IsBackgroundTaskRegistered()
//        {
//            var status = BackgroundExecutionManager.GetAccessStatus();
//            var task = BackgroundTaskRegistration.AllTasks.FirstOrDefault(x => x.Value.Name == UpdateTaskName);
//            return task.Key != Guid.Empty && status != BackgroundAccessStatus.Denied;
//        }

//        public bool IsHasAccess()
//        {
//            var access = BackgroundExecutionManager.GetAccessStatus();
//            return access != BackgroundAccessStatus.Denied;
//        }

//        public void RemoveAccess()
//        {
//            BackgroundExecutionManager.RemoveAccess();
//        }
        
//    }
//}
