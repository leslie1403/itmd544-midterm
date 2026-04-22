import { client } from './client/client.gen';
import {
  bookServiceList,
  bookServiceCreate,
  bookServiceDelete,
  bookServiceStats
} from './client/sdk.gen';

client.setConfig({
  baseUrl: window.location.origin
});

// DOM Elements
const bookListEl = document.getElementById('bookList')!;
const createBookForm = document.getElementById('createBookForm') as HTMLFormElement;
const loadStatsBtn = document.getElementById('loadStatsBtn')!;
const statsResult = document.getElementById('statsResult')!;
const statsOutput = document.getElementById('statsOutput')!;

// Load all books
async function loadBooks() {
  try {
    const { data, error } = await bookServiceList();
    if (error) {
      console.error('Error fetching books', error);
      bookListEl.innerHTML = `<p style="color: red;">Error loading books</p>`;
      return;
    }

    if (!data || data.length === 0) {
      bookListEl.innerHTML = `<p>No books available.</p>`;
      return;
    }

    bookListEl.innerHTML = '';
    data.forEach(book => {
      const div = document.createElement('div');
      div.className = 'book-item';
      div.innerHTML = `
        <div>
          <strong>${book.title}</strong> by ${book.author}<br/>
          <small>${book.publishedYear} &middot; ${book.genre} &middot; ISBN: ${book.isbn}</small>
        </div>
        <button class="btn-danger" data-id="${book.id}">Delete</button>
      `;
      bookListEl.appendChild(div);
    });

    // Attach delete listeners
    const deleteBtns = bookListEl.querySelectorAll('.btn-danger');
    deleteBtns.forEach(btn => {
      btn.addEventListener('click', async (e) => {
        const id = (e.target as HTMLButtonElement).getAttribute('data-id');
        if (id) await deleteBook(id);
      });
    });

  } catch (err) {
    console.error(err);
    bookListEl.innerHTML = `<p style="color: red;">Error loading books</p>`;
  }
}

// Create book
createBookForm.addEventListener('submit', async (e) => {
  e.preventDefault();
  const title = (document.getElementById('title') as HTMLInputElement).value;
  const author = (document.getElementById('author') as HTMLInputElement).value;
  const isbn = (document.getElementById('isbn') as HTMLInputElement).value;
  const genre = (document.getElementById('genre') as HTMLSelectElement).value;
  const publishedYear = parseInt((document.getElementById('publishedYear') as HTMLInputElement).value, 10);

  try {
    const { error } = await bookServiceCreate({
      body: { title, author, isbn, genre: genre as any, publishedYear }
    });

    if (error) {
      alert(`Error creating book: ${JSON.stringify(error)}`);
    } else {
      createBookForm.reset();
      await loadBooks();
    }
  } catch (err) {
    console.error(err);
    alert('Failed to create book');
  }
});

// Delete book
async function deleteBook(id: string) {
  try {
    const { error } = await bookServiceDelete({ path: { id } });
    if (error) {
      alert(`Error deleting book: ${JSON.stringify(error)}`);
    } else {
      await loadBooks();
    }
  } catch (err) {
    console.error(err);
    alert('Failed to delete book');
  }
}

// Load stats (Custom operation)
loadStatsBtn.addEventListener('click', async () => {
  try {
    const { data, error } = await bookServiceStats();
    if (error) {
      alert('Error fetching stats');
    } else {
      statsResult.style.display = 'block';
      statsOutput.textContent = JSON.stringify(data, null, 2);
    }
  } catch (err) {
    console.error(err);
    alert('Failed to load stats');
  }
});

// Initialize
loadBooks();
