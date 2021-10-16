import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'nameFilter'})
export class NameFilterPipe implements PipeTransform{

    /**
     * Aplica un filtro en una lista de arrays
     * @param items Array con los items en los que se aplicará el filtro. 
     * El objeto del array debe tener la propiedad nombre
     * @param searchText Texto a buscar
     * @returns Array con los items cuyo nombre coincida con el texto
     */
    transform(items: any[], searchText: string): any[] {
        if (!items){
            return [];
        }

        if (!searchText){
            return items;
        }

        searchText = searchText.toLocaleLowerCase();

        return items.filter( i => {
            return i.name.toLocaleLowerCase().includes(searchText);
        })
    }
}