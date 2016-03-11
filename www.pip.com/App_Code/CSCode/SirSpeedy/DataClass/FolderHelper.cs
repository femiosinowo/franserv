using System.Collections.Generic;

using Ektron.Cms;
using Ektron.Cms.Framework.Organization;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// Summary description for FolderHelper
    /// </summary>
    public class FolderHelper
    {
        private static FolderManager fMngr = new FolderManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public FolderHelper()
        {
            //constructor
        }

        public static FolderData GetItem(long folderId)
        {
            return fMngr.GetItem(folderId);
        }

        public static void DeleteFolder(long folderId)
        {
            fMngr.Delete(folderId);
        }

        public static FolderData GetTree(long folderId)
        {
            return fMngr.GetTree(folderId);
        }

        public static List<FolderData> GetList(FolderCriteria folderCriteria)
        {
            return fMngr.GetList(folderCriteria);
        }

        public static FolderData Add(FolderData folderData)
        {
            return fMngr.Add(folderData);
        }

        public static FolderData Update(FolderData folderData)
        {
            return fMngr.Update(folderData);
        }
    }
}