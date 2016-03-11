using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ektron.Cms;
using Ektron.Cms.Organization;
using Ektron.Cms.Framework.Organization;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for MenuHelper
    /// </summary>
    public class MenuHelper
    {
        private static MenuManager menuMngr = new MenuManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public MenuHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Method to get menu data by menu id
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static MenuData GetMenuData(long menuId)
        {
            MenuData mData = null;
            if (menuId > 0)
            {
                mData = menuMngr.GetMenu(menuId);
            }
            return mData;
        }

        /// <summary>
        /// This method is used to get menu tree with child items
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static MenuData GetMenuTree(long menuId)
        {
            MenuData mData = null;
            if (menuId > 0)
            {
                mData = menuMngr.GetTree(menuId);
            }
            return mData;
        }

        /// <summary>
        /// This method is used to get menu data list by
        /// </summary>
        /// <param name="mCriteria"></param>
        /// <returns></returns>
        public static List<MenuData> GetMenuTree(MenuCriteria mCriteria)
        {
            List<MenuData> mList = null;
            if (mCriteria != null)
            {
                mList = menuMngr.GetMenuList(mCriteria);
            }
            return mList;
        }

    }
}