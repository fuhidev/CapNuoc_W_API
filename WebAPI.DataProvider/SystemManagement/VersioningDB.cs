using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;

/**
* @author phuonghieuho
*
* @date - 11/8/2018 4:15:20 PM 
*/

namespace WebAPI.DataProvider.SystemManagement
{
    public class VersioningDB : IAction<SYS_Version, string>
    {
        public SYS_Version CheckUpdate(string applicationId, string version)
        {
            using (var context = new SystemEntities())
            {
                var currentModel = this.Get(version);
                if (currentModel == null) { return null; }
                if (currentModel == null) { return null; }
                var lastVersion = context.SYS_Version
                    .Where(w => w.ApplicationId.Equals(applicationId))
                    .OrderByDescending(o => o.Date)
                    .Take(1)
                    .FirstOrDefault();
                if (lastVersion != null)
                {
                    // nếu là version mới
                    if (currentModel.Date < lastVersion.Date)
                    {
                        return lastVersion;
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

        public SYS_Version Create(SYS_Version model)
        {
            using (var context = new SystemEntities())
            {
                if (model != null)
                {
                    var addModel = context.SYS_Version.Add(model);
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

        public bool Delete(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_Version.FirstOrDefault(f => f.VersionCode.Equals(id));
                    if (model != null)
                    {
                        context.SYS_Version.Remove(model);
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

        public SYS_Version Get(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Version.FirstOrDefault(f => f.VersionCode.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_Version> GetAll()
        {
            using (var context = new SystemEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.SYS_Version.ToList();
            }
        }

        public bool Update(string id, SYS_Version model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            using (var context = new SystemEntities())
            {
                var baseModel = context.SYS_Version.FirstOrDefault(f => f.VersionCode.Equals(id));
                if (baseModel == null)
                {
                    throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                }
                else
                {
                    model.VersionCode = id;
                    context.Entry(baseModel).CurrentValues.SetValues(model);
                    //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    //context.SYS_Layer.Attach(model);
                    return context.SaveChanges() > 0;
                }
            }
        }
    }
}
