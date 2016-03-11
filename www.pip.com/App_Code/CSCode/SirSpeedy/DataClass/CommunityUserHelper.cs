using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Framework.Settings;
using Ektron.Cms.Framework.User;
using Ektron.Cms.User;

namespace SirSpeedy.CMS
{

    /// <summary>
    /// Summary description for CommunityUserHelper
    /// </summary>
    public class CommunityUserHelper
    {
        private static UserGroupManager userGroupManager = new UserGroupManager(Ektron.Cms.Framework.ApiAccessMode.Admin);
        private static UserManager userManager = new UserManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public CommunityUserHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool IsCmsUserExist(string userName)
        {
            bool status = true;
            var criteria = new UserCriteria();
            criteria.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, userName);
            criteria.AddFilter(UserProperty.IsMemberShip, CriteriaFilterOperator.EqualTo, true);
            var userList = userManager.GetList(criteria);
            if (userList != null && userList.Count > 0)
                status = true;
            else
                status = false;

            return status;
        }

        public static void DeleteUser(long userId)
        {
            userManager.Delete(userId);
        }

        public static void DeleteUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var uData = GetUserByUserName(userName);
                DeleteUser(uData.Id);
            }
        }

        public static UserData GetUserByUserName(string userName)
        {
            UserData uData = null;
            UserCriteria uc = new UserCriteria();
            uc.AddFilter(UserProperty.UserName, CriteriaFilterOperator.EqualTo, userName);
            var userList = userManager.GetList(uc);
            if (userList != null && userList.Count > 0)
            {
                uData = userList[0];
            }
            return uData;
        }

        public static UserData GetUserByUserId(long id)
        {
            UserData uData = null;
            UserCriteria uc = new UserCriteria();
            uc.AddFilter(UserProperty.Id, CriteriaFilterOperator.EqualTo, id);
            var userList = userManager.GetList(uc);
            if (userList != null && userList.Count > 0)
            {
                uData = userList[0];
            }
            return uData;
        }

        public static long AddCmsMembershipUser(string firstName, string lastName, string userName, string password, string email, string centerId)
        {
            long userId = 0;
            var userData = new UserData();           
            UserCriteria userCriteria = new UserCriteria();
            userData.FirstName = firstName;
            userData.LastName = lastName;
            userData.Email = email;
            userData.Username = userName;
            userData.Password = password;
            userData.DisplayName = userName;
            userData.IsMemberShip = true;
            userData.CustomProperties = userManager.GetCustomPropertyList();
            userData.CustomProperties["Time Zone"].Value = "US Eastern Standard Time";
            userCriteria.AddFilter(UserProperty.Email, CriteriaFilterOperator.EqualTo, email);
            List<UserData> uData = userManager.GetList(userCriteria);
            if (uData != null && uData.Count == 0)
            {
                var resultData = userManager.Add(userData);

                if (resultData != null && resultData.Id > 0)
                {
                    userId = resultData.Id;
                    //todo: if issues found add centerId to custom properties too
                }
                else
                {
                    userId = 0;
                }
            }
            return userId;
        }

        public static long AddCommunityGroup(string groupName, string phoneNumber)
        {
            long status = 0;
            if (!string.IsNullOrEmpty(groupName))
            {
                UserGroupData UserGroupdata = new UserGroupData()
                {
                    GroupName = groupName,
                    IsMemberShipGroup = true,
                };
                UserGroupCriteria criteria = new UserGroupCriteria();
                criteria.AddFilter(UserGroupProperty.Name, CriteriaFilterOperator.EqualTo, groupName);
                var list = userGroupManager.GetList(criteria);
                if (list != null && list.Count > 0)
                {
                    status = list[0].Id;
                }
                else
                {
                    var groupData = userGroupManager.Add(UserGroupdata);
                    if (groupData != null && groupData.Id > 0)
                        status = groupData.Id;
                }
            }
            return status;
        }

        public static void AddUserToCommunityGroup(long cmsUserId, long userGroupId, string centerId, string centerPhoneNumber)
        {
            if (cmsUserId > 0)
            {
                if (userGroupId > 0)
                {
                    userGroupManager.AddUser(userGroupId, cmsUserId);
                }
                else
                {
                    long groupId = AddCommunityGroup(centerId, centerPhoneNumber);
                    if (groupId > 0)
                    {
                        userGroupManager.AddUser(groupId, cmsUserId);
                        FransDataManager.AddCmsGroupToCenter(centerId, groupId);
                    }
                }
            }
        }

        public static void LockUserAccount(string userName)
        {
            if(!string.IsNullOrEmpty(userName))
            {
                var userData = GetUserByUserName(userName);
                if(userData != null && userData.Id > 0)
                {
                    userManager.LockUser(userData.Id);
                }
            }
        }

        public static void UnLockUserAccount(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                var userData = GetUserByUserName(userName);
                if (userData != null && userData.Id > 0)
                {
                    userManager.UnlockUser(userData.Id);
                }
            }
        }

    }
}