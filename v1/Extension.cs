
using V1.Dtos;
using V1.Entity;

namespace V1.Service{

    public static class Extension{

        public static ItemDto AsDto(this Item item){

            return new ItemDto(item.Id, item.Name,item.Description,item.Price, item.CreatedDate);

        }



    }




}