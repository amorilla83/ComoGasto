export interface Pagination <T> {
    items: T[];
    itemsPerPage: number;
    countItems: number;
    countPage: number;
    pageNumber: number;
  }