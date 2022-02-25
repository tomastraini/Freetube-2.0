import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tableFilter'
})
export class TableFilterPipe implements PipeTransform {

  transform(li: any[], value: string) {
   return value ? li.filter(li => 
      li.description.includes(value) |
      li.title.includes(value) |
      li.title.toLowerCase().includes(value.toLowerCase()) |
      li.description.toLowerCase().includes(value.toLowerCase())
    ) : li;
  }
}   