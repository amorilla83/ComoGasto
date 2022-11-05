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
        //Quitar tildes

        searchText = this.quitarAcentos(searchText);

        return items.filter( i => {
            let name : string = i.name;
            name = this.quitarAcentos (name);
            return name.toLocaleLowerCase().includes(searchText);
        })
    }

    quitarAcentos(cadena: string) : string{
        const acentos = {'á':'a','é':'e','í':'i','ó':'o','ú':'u','Á':'A','É':'E','Í':'I','Ó':'O','Ú':'U'};
        return cadena.split('').map( letra => acentos[letra] || letra).join('').toString();	
    }
}