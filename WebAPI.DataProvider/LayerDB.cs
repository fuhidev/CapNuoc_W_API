using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataProvider.EF;
using WebAPI.DataProvider.Models;

namespace WebAPI.DataProvider
{
    public class LayerDB : IAction<SYS_Layer, string>
    {
        public SYS_Layer Create(SYS_Layer model)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    if (model != null)
                    {
                        var addModel = context.SYS_Layer.Add(model);
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

        public bool Delete(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_Layer.FirstOrDefault(f => f.ID.Equals(id));
                    if (model != null)
                    {
                        context.SYS_Layer.Remove(model);
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

        public SYS_Layer Get(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Layer.FirstOrDefault(f => f.ID.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_Layer> GetAll()
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_Layer.Include("SYS_GroupLayer").ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, SYS_Layer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new SystemEntities())
                {
                    var baseModel = context.SYS_Layer.FirstOrDefault(f => f.ID.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.ID = id;
                        context.Entry(baseModel).CurrentValues.SetValues(model);
                        //context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        //context.SYS_Layer.Attach(model);
                        return context.SaveChanges() > 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class GroupLayerDB : IAction<SYS_GroupLayer, string>
    {
        public SYS_GroupLayer Create(SYS_GroupLayer model)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    if (model != null)
                    {
                        var addModel = context.SYS_GroupLayer.Add(model);
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

        public bool Delete(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    var model = context.SYS_GroupLayer.FirstOrDefault(f => f.ID.Equals(id));
                    if (model != null)
                    {
                        context.SYS_GroupLayer.Remove(model);
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

        public SYS_GroupLayer Get(string id)
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_GroupLayer.FirstOrDefault(f => f.ID.Equals(id));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<SYS_GroupLayer> GetAll()
        {
            try
            {
                using (var context = new SystemEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return context.SYS_GroupLayer.ToList();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(string id, SYS_GroupLayer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            try
            {
                using (var context = new SystemEntities())
                {
                    var baseModel = context.SYS_GroupLayer.FirstOrDefault(f => f.ID.Equals(id));
                    if (baseModel == null)
                    {
                        throw new NullReferenceException("Không tìm thấy đối tượng trong cơ sở dữ liệu");
                    }
                    else
                    {
                        model.ID = id;
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
    }
}
