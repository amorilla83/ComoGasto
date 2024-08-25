## Update nodejs

Descargarlo de la página y ejecutar el instalador 

## Update angular

ng update @angular/cli @angular/core

npm update -g
npm update
Si da errores de dependencias, cambiar en el package.json a la versión que está pidiendo

## Update Bootstrap

ng update @ng-bootstrap/ng-bootstrap

## DS_Store
Si da problemas con el DS_Store, eliminarlo con el siguiente comando

find `npm list -g | head -1` -name '.DS_Store' -type f -delete