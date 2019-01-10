using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class AccountDB : IAction<SYS_Account, string>
    {
        public List<LayerInfo> LayerInfos(SYS_Account account)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var query = from la in context.SYS_Layer_Account
                                join lyr in context.SYS_Layer on la.Layer equals lyr.ID
                                where la.Account == account.Username
                                orderby lyr.NumericalOder
                                select new LayerInfo
                                {
                                    LayerID = lyr.ID,
                                    LayerTitle = lyr.Title,
                                    IsView = la.IsView.HasValue ? la.IsView.Value : false,
                                    IsCreate = la.IsCreate.HasValue ? la.IsCreate.Value : false,
                                    IsDelete = la.IsDelete.HasValue ? la.IsDelete.Value : false,
                                    IsEdit = la.IsEdit.HasValue ? la.IsEdit.Value : false,
                                    Definition = String.IsNullOrEmpty(la.Definition) ? null : la.Definition.Replace("\"", "'"),
                                    Url = lyr.Url,
                                    OutFields = la.OutFields,
                                    QueryFields = la.QueryFields,
                                    UpdateFields = la.UpdateFields,
                                    GroupLayer = String.IsNullOrEmpty(lyr.SYS_GroupLayer.ID) ? null : new GroupLayer
                                    {
                                        ID = lyr.SYS_GroupLayer.ID,
                                        Name = lyr.SYS_GroupLayer.Name
                                    }
                                };

                    return query.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public SYS_Account IsValid(SYS_Account account)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var password = Helper.MD5.CryptoPassword(account.Password);
                    var sysAccount = context.SYS_Account.
                        FirstOrDefault(
                        f =>
                        f.Username.Equals(account.Username, StringComparison.OrdinalIgnoreCase)
                        && f.Password.Equals(password));
                    return sysAccount;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_Account> GetAll()
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false; // cho phép request relationship

                    return context.SYS_Account
                        .Include("SYS_Role")
                        .Include("SYS_Capability_Account").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SYS_Account Get(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false; // cho phép request relationship
                    return context.SYS_Account.Include("SYS_Role").Include("SYS_Capability_Account").FirstOrDefault(f => f.Username.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_Account.FirstOrDefault(f => f.Username.Equals(id));
                    if (model != null)
                    {
                        context.SYS_Account.Remove(model);
                        return context.SaveChanges() > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, SYS_Account model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new SystemEntities())
                {
                    var baseModel = context.SYS_Account.FirstOrDefault(f => f.Username.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.Username = id;
                        context.Entry(baseModel).CurrentValues.SetValues(model);
                        return context.SaveChanges() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SYS_Account Create(SYS_Account model)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    if (model != null)
                    {
                        var addModel = context.SYS_Account.Add(model);
                        var result = context.SaveChanges();
                        if (result > 0)
                        {
                            return addModel;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
