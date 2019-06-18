using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webjob
{
    class Program
    {
        static void Main(string[] args)
        {
            using (demoazureEntities obj = new demoazureEntities())
            {
                if (obj.polls.Count() > 0)
                {
                    obj.polls.RemoveRange(obj.polls.ToList());
                }

                else
                {
                    for (int count = 0; count < 10; count++)
                    {
                        obj.polls.Add(new polls { name = "name -" + count });
                    }
                }
                obj.SaveChanges();

            
            }
           
        }
    }
}
