import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
  });

  it('loads and displays the available books', () => {
    const fixture = TestBed.createComponent(App);
    const http = TestBed.inject(HttpTestingController);

    fixture.detectChanges();
    http.expectOne('/books').flush([
      { id: 'book-1', title: 'The Dispossessed', pageCount: 387, price: 14.5 },
    ]);
    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('The Dispossessed');
    expect(fixture.nativeElement.textContent).toContain('387 pages');
    http.verify();
  });
});
