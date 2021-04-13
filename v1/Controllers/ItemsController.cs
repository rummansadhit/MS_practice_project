


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using V1.Dtos;
using V1.Entity;
using V1.Repositories;
using V1.Service;

namespace V1.Controllers
{
    
[ApiController]
[Route("items")]
public class ItemsController:ControllerBase
{

    private readonly ItemsRepository itemsRepository = new();


    [HttpGet]

    public async Task<IEnumerable<ItemDto>> GetAsync()
    {

        var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto() );

        return items;

    }

    [HttpGet("{id}")]

    public async Task<ActionResult<ItemDto>> GetIdAsync(Guid id){

            var item = await itemsRepository.GetAsync(id);

            if(item == null){

                return NotFound();
            }

            else{

                return item.AsDto();
            }

    }

    [HttpPost]

    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto){

        var item=new Item{

                Name=createItemDto.Name,
                Price=createItemDto.Price,
                Description=createItemDto.Description,
                CreatedDate= DateTimeOffset.UtcNow
                };

        await itemsRepository.CreateAsync(item);

        return CreatedAtAction(nameof(GetIdAsync), new{id = item.Id},item);


    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto){

            var existingItem = await itemsRepository.GetAsync(id);

            if(existingItem == null){

                return NotFound();

            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Price= updateItemDto.Price;
            existingItem.Description = updateItemDto.Description;
            
            await itemsRepository.UpdateAsync(existingItem);

            return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id){

        var item = await itemsRepository.GetAsync(id);

        if(item == null){

            return NotFound();
        }

        await itemsRepository.RemoveAsync(id);

        return NoContent();


    }





}

}