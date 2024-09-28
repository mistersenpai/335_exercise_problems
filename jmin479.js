const navHome = document.getElementById("navhome");
const navNZSL = document.getElementById("navNZSL");
const navEvents = document.getElementById("navevents");
const navRegister = document.getElementById("navreg");
const navLogin = document.getElementById("navlogin");
const navGuestBook = document.getElementById("navgbook");
const logoutButton = document.getElementById("logout_button");
const loggedInStatus = document.getElementById("logged_in");

const sectionHome = document.getElementById("home");
const sectionNZSL = document.getElementById("NZSL");
const sectionEvents = document.getElementById("events");
const sectionRegister = document.getElementById("reg");
const sectionLogin = document.getElementById("login");
const sectionGuestBook = document.getElementById("gbook");

logoutButton.style.display = 'none';
loggedInStatus.style.display = 'none';

const hideAllSections = () => {
    sectionHome.style.display = "none";
    sectionNZSL.style.display = "none";
    sectionEvents.style.display = "none";
    sectionRegister.style.display = "none";
    sectionLogin.style.display = "none";
    sectionGuestBook.style.display = "none";
};

const showHome = () => { hideAllSections(); sectionHome.style.display = "block"; };
const showNZSL = () => { hideAllSections(); sectionNZSL.style.display = "block"; loadSigns(); };
const showEvents = () => { hideAllSections(); sectionEvents.style.display = "block"; getEvents(); };
const showRegister = () => { hideAllSections(); sectionRegister.style.display = "block"; };
const showLogin = () => { hideAllSections(); sectionLogin.style.display = "block"; };

navHome.addEventListener("click", showHome);
navNZSL.addEventListener("click", showNZSL);
navEvents.addEventListener("click", showEvents);
navRegister.addEventListener("click", showRegister);
navLogin.addEventListener("click", showLogin);

/**
 * Fetches the application version from the API and displays it on the homepage.
 */
function loadVersion() {
    const versionElement = document.getElementById('versionText');
    
    fetch('https://cws.auckland.ac.nz/nzsl/api/Version')
        .then(response => response.text())
        .then(version => {
            versionElement.textContent = `Version: ${version}`;
        })
        .catch(error => {
            console.error('Error fetching version:', error);
            versionElement.textContent = "Version: Unavailable";
        });
}

// Call this function when the homepage loads
navHome.addEventListener("click", loadVersion);

// Initially load version when homepage is the first visible section
loadVersion();

/**
 * Fetches and loads all NZSL signs from the API.
 */
function loadSigns() {
    const signsContainer = document.getElementById("NZSLsigns");

    // Fetch all signs
    fetch("https://cws.auckland.ac.nz/nzsl/api/AllSigns", {
        headers: { "Accept": "application/json" },
    })
    .then(response => response.json())
    .then(signData => {
        signsContainer.innerHTML = "";  // Clear previous content

        // Dynamically create a card for each sign and append to container
        signData.forEach(sign => {
            signsContainer.innerHTML += `
                <div class="sign-item">
                    <h3>${sign.description}</h3>
                    <img src='https://cws.auckland.ac.nz/nzsl/api/SignImage/${sign.id}' alt='${sign.description}' />
                </div>
            `;
        });
    })
    .catch(error => {
        console.error("Error fetching signs:", error);
        signsContainer.innerHTML = "<p>Failed to load signs. Please try again later.</p>";
    });
}

/**
 * Handles searching for NZSL signs based on the search input.
 */
function signSearch() {
    const searchTerm = document.getElementById("sign_search").value.trim();
    const signsContainer = document.getElementById("NZSLsigns");

    // If search is empty, reload all signs
    if (searchTerm === '') {
        loadSigns();
        return;
    }

    // Fetch signs based on search term
    fetch(`https://cws.auckland.ac.nz/nzsl/api/Signs/${searchTerm}`, {
        headers: { "Accept": "application/json" }
    })
    .then(response => response.json())
    .then(data => {
        signsContainer.innerHTML = '';  // Clear previous content

        if (data.length === 0) {
            signsContainer.innerHTML = '<p>No signs found for this term.</p>';
        } else {
            // Create a card for each filtered sign
            data.forEach(sign => {
                const signElement = document.createElement('div');
                signElement.classList.add('sign-item');

                const descriptionElement = document.createElement('h3');
                descriptionElement.innerText = sign.description;

                const imageElement = document.createElement('img');
                imageElement.alt = sign.description;
                imageElement.src = `https://cws.auckland.ac.nz/nzsl/api/SignImage/${sign.id}`;

                signElement.appendChild(descriptionElement);
                signElement.appendChild(imageElement);
                signsContainer.appendChild(signElement);
            });
        }
    })
    .catch(error => {
        console.error('Error fetching signs:', error);
        signsContainer.innerHTML = "<p>Failed to fetch signs. Please try again later.</p>";
    });
}

// Call the loadSigns function when the NZSL section is shown
navNZSL.addEventListener("click", loadSigns);
/**
 * Helper function to parse the iCal date string into a JavaScript Date object.
 */
function parseICalDate(calDate) {
    const year = calDate.slice(0, 4);
    const month = calDate.slice(4, 6) - 1; // Months are 0-indexed in JavaScript
    const day = calDate.slice(6, 8);
    const hour = calDate.slice(9, 11);
    const minute = calDate.slice(11, 13);
    const second = calDate.slice(13, 15);
    return new Date(Date.UTC(year, month, day, hour, minute, second));
}

/**
 * Formats the date into "3 Sep 2024".
 */
function formatDate(dateString) {
    const date = parseICalDate(dateString);
    const options = { day: 'numeric', month: 'short', year: 'numeric' };
    return date.toLocaleDateString(undefined, options);
}

/**
 * Formats the time into "2:00 PM".
 */
function formatTime(dateString) {
    const date = parseICalDate(dateString);
    return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
}

/**
 * Parses the iCal event string to extract relevant information.
 */
function parseEvent(ical) {
    const event = {};
    const lines = ical.split("\n");
    lines.forEach(line => {
        if (line.startsWith("SUMMARY:")) {
            event.name = line.replace("SUMMARY:", "").trim();
        }
        if (line.startsWith("DESCRIPTION:")) {
            event.description = line.replace("DESCRIPTION:", "").trim();
        }
        if (line.startsWith("DTSTART:")) {
            event.start = line.replace("DTSTART:", "").trim();
        }
        if (line.startsWith("DTEND:")) {
            event.end = line.replace("DTEND:", "").trim();
        }
        if (line.startsWith("LOCATION:")) {
            event.location = line.replace("LOCATION:", "").trim();
        }
    });
    return event;
}

/**
 * Downloads the event as an .ics file.
 */
function downloadICS(eventId) {
    fetch(`https://cws.auckland.ac.nz/nzsl/api/Event/${eventId}`)
        .then(response => response.text())
        .then(eventData => {
            const blob = new Blob([eventData], { type: 'text/calendar' });
            const url = URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = url;
            link.download = `event-${eventId}.ics`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        })
        .catch(error => console.error('Error downloading ICS file:', error));
}

/**
 * Main function to fetch events and display them.
 */
let eventsLoaded = false; // Flag to track if events have been loaded

function getEvents() {
    if (eventsLoaded) return; // If already loaded, don't fetch again
    eventsLoaded = true; // Mark events as loaded

    fetch('https://cws.auckland.ac.nz/nzsl/api/EventCount')
        .then(response => response.json())
        .then(eventCount => {
            const eventsList = document.getElementById('events_sec');
            eventsList.innerHTML = ''; // Clear any previous events

            if (eventCount === 0) {
                eventsList.innerHTML = '<li>No events available at this time.</li>';
                return;
            }

            // Fetch each event based on the count
            for (let i = 0; i < eventCount; i++) {
                fetch(`https://cws.auckland.ac.nz/nzsl/api/Event/${i}`)
                    .then(response => response.text()) // Expect iCal text response
                    .then(eventData => {
                        const event = parseEvent(eventData);
                        const startDate = formatDate(event.start);
                        const startTime = formatTime(event.start);
                        const endTime = formatTime(event.end);

                        // Create event card layout
                        const eventElement = document.createElement('li');
                        eventElement.innerHTML = `
                            <div class="event-card">
                                <div class="event-date">
                                    <div class="date-day">${startDate.split(" ")[0]}</div>
                                    <div class="date-month-year">${startDate.split(" ")[1]} ${startDate.split(" ")[2]}</div>
                                </div>
                                <div class="event-details">
                                    <h3>${event.name}</h3>
                                    <p>${event.description}</p>
                                    <p><strong>Location:</strong> ${event.location}</p>
                                    <p><strong>Starts:</strong> ${startDate}, ${startTime}</p>
                                    <p><strong>Finishes:</strong> ${startDate}, ${endTime}</p>
                                    <button class="add-to-calendar" onclick="downloadICS('${i}')">+</button>
                                </div>
                            </div>
                        `;
                        eventsList.appendChild(eventElement);
                    })
                    .catch(error => console.error(`Error fetching event ${i}:`, error));
            }
        })
        .catch(error => console.error('Error fetching event count:', error));
}

// Trigger the events loading function when the Events section is clicked
// navEvents.addEventListener("click", getEvents);


// register

document.getElementById("register_section").addEventListener("submit", (event) => {
    event.preventDefault();
    
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const address = document.getElementById('address').value;
    const registerMessage = document.getElementById('registerMessage');
    
    fetch('https://cws.auckland.ac.nz/nzsl/api/Register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password, address })
    })
    .then(response => response.text())
    .then(data => {
        if (data.includes('Username not available')) {
            registerMessage.innerText = `Username ${username} is not available.`;
        } else {
            registerMessage.innerText = `User ${username} registered.`;
            document.getElementById("register_section").reset();  // Clears form inputs
        }
    })
    .catch(error => {
        registerMessage.innerText = 'An error occurred. Please try again.';
        console.error('Error:', error);
    });
});


// login/out stuff 
document.getElementById("login_section").addEventListener("submit", (event) => {
    event.preventDefault();

    const username = document.getElementById('L-username').value;
    const password = document.getElementById('L-password').value;
    const loginMessage = document.getElementById('loginMessage');
    
    const authHeader = 'Basic ' + btoa(username + ':' + password);

    // Fetch the login API with authentication
    fetch('https://cws.auckland.ac.nz/nzsl/api/TestAuth', {
        method: 'GET',
        headers: { 'Authorization': authHeader } 
    })
    .then(response => {
        if (response.ok) {
            localStorage.setItem('authHeader', authHeader);  // Store auth header in localStorage
            localStorage.setItem('username', username);  // Store username for future use
            loginMessage.innerText = `User ${username} logged in.`;
            document.getElementById("logoutBtn").style.display = "block";  // Show logout button
            document.getElementById("login_section").reset();  // Clear the form
        } else if (response.status === 401) {
            loginMessage.innerText = 'Incorrect username or password.';
        } else {
            throw new Error('Unexpected error during login.');
        }
    })
    .catch(error => {
        console.error('Error during login:', error);
        loginMessage.innerText = 'An error occurred. Please try again.';
    });
});

// Logout Functionality
document.getElementById("logoutBtn").addEventListener("click", () => {
    localStorage.removeItem('authHeader');  // Clear stored auth header
    localStorage.removeItem('username');  // Clear stored username
    const loginMessage = document.getElementById('loginMessage');
    loginMessage.innerText = 'Log in here.';  // Reset login message
    document.getElementById("logoutBtn").style.display = "none";  // Hide logout button
});

//comments

function loadComments() {
    const commentsList = document.getElementById("commentsList");

    // Add debug log to ensure this function is being called
    console.log("Loading comments...");

    // Fetch the comments from the API (which returns HTML)
    fetch('https://cws.auckland.ac.nz/nzsl/api/Comments', {
        method: 'GET',
        headers: { 'Content-Type': 'text/html' },  // Use text/html as content type
    })
    .then(response => response.text())  // Parse as text, not JSON
    .then(htmlData => {
        console.log("HTML Data fetched:", htmlData);  // Log the fetched HTML

        // Create a temporary DOM element to parse the HTML string
        const tempDiv = document.createElement("div");
        tempDiv.innerHTML = htmlData;

        // Select all <p> tags from the HTML (where comments are located)
        const comments = tempDiv.querySelectorAll("p");

        // Clear the current comments section
        commentsList.innerHTML = "";

        if (comments.length === 0) {
            commentsList.innerHTML = "<li>No comments yet. Be the first to comment!</li>";
        } else {
            // Loop through all <p> elements and extract the comment text
            comments.forEach(commentElement => {
                const commentText = commentElement.innerText;
                const listItem = document.createElement("li");
                listItem.textContent = commentText;
                commentsList.appendChild(listItem);
            });
        }
    })
    .catch(error => {
        console.error('Error fetching comments:', error);
        commentsList.innerHTML = "<li>Failed to load comments. Please try again later.</li>";
    });
}

// Call this function when the guestbook page is shown to load all comments
function showGuestBook() {
    hideAllSections();
    sectionGuestBook.style.display = "block";
    loadComments(); // Load comments when the guestbook page is opened
}

// Attach event listener for the guestbook nav
navGuestBook.addEventListener("click", showGuestBook);
// Ensure that the submit button is correctly attached to the event listener
document.getElementById("submit_comment").addEventListener("click", submitComment);

// Function to handle comment submission
function submitComment() {
    const commentText = document.getElementById("commentText").value.trim();  // Trim input
    const commentMessage = document.getElementById("commentMessage");
    const commentsList = document.getElementById("commentsList");
    const authHeader = localStorage.getItem('authHeader');  // Get auth header

    console.log("Submit button clicked!");  // Debugging step

    // Check if user is logged in
    if (!authHeader) {
        commentMessage.innerText = "Please log in to submit a comment.";
        showLogin();  // Redirect to login if not logged in
        return;
    }

    if (commentText === "") {
        commentMessage.innerText = "Comment cannot be empty.";
        return;
    }

    // Post comment to the API as plain text (no JSON formatting)
    fetch('https://cws.auckland.ac.nz/nzsl/api/Comment', {
        method: 'POST',
        headers: { 
            'Authorization': authHeader,
            'Content-Type': 'text/plain'  // Sending as plain text
        },
        body: commentText  // Sending comment as plain text
    })
    .then(response => {
        if (response.ok) {
            console.log("Comment submitted successfully!");
            document.getElementById("commentText").value = "";  // Clear the textarea
            commentMessage.innerText = "Comment submitted successfully.";

            // Add the new comment to the top of the list
            const listItem = document.createElement("li");
            listItem.textContent = `${commentText} — ${localStorage.getItem('username')}`;
            commentsList.insertBefore(listItem, commentsList.firstChild);  // Insert at the top
        } else {
            return response.text().then(text => {
                console.error('Failed to submit comment:', text);
                throw new Error('Failed to submit comment. Server response: ' + text);
            });
        }
    })
    .catch(error => {
        console.error('Error submitting comment:', error);
        commentMessage.innerText = "Failed to submit comment. Please try again.";
    });
}