using Microsoft.EntityFrameworkCore;

namespace _mvcproject_.Models
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Service_Id = 1,
                    Service_Name = "Carpentry" ,
                    ImgPath = "servicelogos/carpenter.jpg"
                },
                new Service
                {
                    Service_Id=2,
                    Service_Name = "Courrier",
                    ImgPath = "servicelogos/courrier.png"
                },
                new Service
                {
                    Service_Id =3 ,
                    Service_Name = "Electronics Fixing",
                    ImgPath = "servicelogos/electronics.jpg"
                }
                );
        }
    }
}
