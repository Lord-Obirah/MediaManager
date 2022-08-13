import {IQueryParameter} from "./queryParameter";

export interface IPaginationHeader {
  nextPageLink?: IQueryParameter,
  previousPageLink?: IQueryParameter,
  currentPageLink: IQueryParameter,
  pageSize?: number,
  currentPage?: number,
  totalCount?: number,
  totalPages?: number
}
