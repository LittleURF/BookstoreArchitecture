import { DecimalPipe } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Book, BookCreateRequest, BooksService } from './books.service';

@Component({
  selector: 'app-root',
  imports: [DecimalPipe, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  protected readonly books = signal<Book[]>([]);
  protected readonly selectedBook = signal<Book | null>(null);
  protected readonly loading = signal(true);
  protected readonly saving = signal(false);
  protected readonly deletingBookId = signal<string | null>(null);
  protected readonly errorMessage = signal<string | null>(null);
  protected readonly successMessage = signal<string | null>(null);

  protected newBook: BookCreateRequest = this.emptyBook();

  constructor(private readonly booksService: BooksService) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  protected loadBooks(): void {
    this.loading.set(true);
    this.errorMessage.set(null);

    this.booksService.getBooks().subscribe({
      next: (books) => {
        this.books.set(books);
        this.loading.set(false);
      },
      error: (error: unknown) => {
        this.errorMessage.set(this.getErrorMessage(error));
        this.loading.set(false);
      },
    });
  }

  protected showDetails(id: string): void {
    this.errorMessage.set(null);
    this.booksService.getBook(id).subscribe({
      next: (book) => this.selectedBook.set(book),
      error: (error: unknown) => this.errorMessage.set(this.getErrorMessage(error)),
    });
  }

  protected closeDetails(): void {
    this.selectedBook.set(null);
  }

  protected addBook(form: NgForm): void {
    if (form.invalid || this.saving()) {
      return;
    }

    const request = { ...this.newBook, title: this.newBook.title.trim() };
    this.saving.set(true);
    this.errorMessage.set(null);
    this.successMessage.set(null);

    this.booksService.createBook(request).subscribe({
      next: (book) => {
        this.books.update((books) => [...books, book]);
        this.newBook = this.emptyBook();
        form.resetForm(this.newBook);
        this.saving.set(false);
        this.successMessage.set(`“${book.title}” was added to the shelf.`);
      },
      error: (error: unknown) => {
        this.errorMessage.set(this.getErrorMessage(error));
        this.saving.set(false);
      },
    });
  }

  protected deleteBook(book: Book): void {
    if (this.deletingBookId() || !window.confirm(`Delete “${book.title}” from the collection?`)) {
      return;
    }

    this.deletingBookId.set(book.id);
    this.errorMessage.set(null);
    this.successMessage.set(null);

    this.booksService.deleteBook(book.id).subscribe({
      next: () => {
        this.books.update((books) => books.filter((item) => item.id !== book.id));
        if (this.selectedBook()?.id === book.id) {
          this.selectedBook.set(null);
        }
        this.deletingBookId.set(null);
        this.successMessage.set(`“${book.title}” was removed from the shelf.`);
      },
      error: (error: unknown) => {
        this.errorMessage.set(this.getErrorMessage(error));
        this.deletingBookId.set(null);
      },
    });
  }

  private emptyBook(): BookCreateRequest {
    return { title: '', pageCount: 1, price: 0.01, currencyCode: 'USD' };
  }

  private getErrorMessage(error: unknown): string {
    if (error instanceof HttpErrorResponse) {
      const problem = error.error as { title?: string; detail?: string } | null;
      return problem?.detail ?? problem?.title ?? error.message;
    }

    return 'Something went wrong. Please try again.';
  }
}
