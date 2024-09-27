// Selecting all pages by their IDs
const homePage = document.getElementById('Home');
const NZSLPage = document.getElementById('NZSL');
const eventsPage = document.getElementById('Events');
const registerPage = document.getElementById('Register');
const guestBookPage = document.getElementById('GuestBook');

// Selecting navigation menu items
const navHome = document.getElementById('navHome');
const navNZSL = document.getElementById('navNZSL');
const navEvents = document.getElementById('navEvents');
const navRegister = document.getElementById('navRegister');
const navGuestBook = document.getElementById('navGuestBook');

// Function to hide all sections
const hideAllSections = () => {
    homePage.style.display = 'none';
    NZSLPage.style.display = 'none';
    eventsPage.style.display = 'none';
    registerPage.style.display = 'none';
    guestBookPage.style.display = 'none';
}

// Functions to show individual sections
const showHome = () => {
    hideAllSections();
    homePage.style.display = 'block';
}

const showNZSL = () => {
    hideAllSections();
    NZSLPage.style.display = 'block';
}

const showEvents = () => {
    hideAllSections();
    eventsPage.style.display = 'block';
}

const showRegister = () => {
    hideAllSections();
    registerPage.style.display = 'block';
}

const showGuestBook = () => {
    hideAllSections();
    guestBookPage.style.display = 'block';
}

// Adding event listeners for the navigation menu
navHome.addEventListener('click', showHome);
navNZSL.addEventListener('click', showNZSL);
navEvents.addEventListener('click', showEvents);
navRegister.addEventListener('click', showRegister);
navGuestBook.addEventListener('click', showGuestBook);
