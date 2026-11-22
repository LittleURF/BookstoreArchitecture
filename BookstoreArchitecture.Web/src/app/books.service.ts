import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Book {
  id: string;
  title: string;
  pageCount: number;
  price: number;
  currencyCode: string;
}

export interface BookCreateRequest {
  title: string;
  pageCount: number;
  price: number;
  currencyCode: string;
}

@Injectable({ providedIn: 'root' })
export class BooksService {
  private readonly http = inject(HttpClient);
  private readonly booksUrl = '/books';

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.booksUrl);
  }

  getBook(id: string): Observable<Book> {
    return this.http.get<Book>(`${this.booksUrl}/${id}`);
  }

  createBook(book: BookCreateRequest): Observable<Book> {
    return this.http.post<Book>(this.booksUrl, book);
  }

  deleteBook(id: string): Observable<void> {
    return this.http.delete<void>(`${this.booksUrl}/${id}`);
  }
}
