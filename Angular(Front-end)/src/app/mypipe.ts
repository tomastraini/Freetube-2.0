import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tableFilter'
})
export class TableFilterPipe implements PipeTransform {

  transform(li: any[], value: string) {
    console.log(li);
    console.log(value);
    
   return value ? li.filter(li => 
    // first check if li.description is null
      li.description !== null ?  li.description.toLowerCase().includes(value.toLowerCase())
      :  li.description.toLowerCase().includes("") ||

      li.title !== null ? li.title.toLowerCase().includes(value.toLowerCase())
      : li.title.toLowerCase().includes("")

    ) : li;
  }
}   