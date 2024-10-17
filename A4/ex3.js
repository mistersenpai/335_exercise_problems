function graph() {
    const graphContainer = document.getElementById("graph");
  
    fetch("https://cws.auckland.ac.nz/nzsl/api/Log").then(response => response.json()).then(entries => {
        // Get the number of entries
        const numEntries = entries.length;
        // Extract data arrays
        const visitsData = entries.map(item => item.visits);
        const uniqueVisitsData = entries.map(item => item.uniqueVisits);
        const datesData = entries.map(item => item.date); 
        // Determine max and min values
        const maxValue = Math.max(...visitsData, ...uniqueVisitsData);
        const minValue = Math.min(...visitsData, ...uniqueVisitsData); 
        // Initialize SVG string
        let svgString = `<svg xmlns="http://www.w3.org/2000/svg" width="${numEntries * 20 + 100}" height="300">`; 
        // Background and title
        svgString += `<rect width="100%" height="100%" fill="white"/>`;
        svgString += `<text x="10" y="20" fill="black" font-family="Verdana" font-size="12" font-weight="bold">Visit Log</text>`; 
        // Y-axis labels (max and min only)
        svgString += `<text x="30" y="30" fill="black" font-family="Verdana" font-size="8">${maxValue}</text>`;
        svgString += `<text x="35" y="150" fill="black" font-family="Verdana" font-size="8">${minValue}</text>`;
        // Initialize paths for lines
        let visitsPath = `M 50 150`;
        let uniqueVisitsPath = `M 50 150`;
        // Build the paths and label dates
        entries.forEach((entry, index) => {
          const xPos = 50 + index * 20;
          // Calculate y positions
          const visitsY = 150 - ((entry.visits - minValue) / (maxValue - minValue)) * 120;
          const uniqueVisitsY = 150 - ((entry.uniqueVisits - minValue) / (maxValue - minValue)) * 120;
          // Add points to paths
          visitsPath += ` L ${xPos} ${visitsY}`;
          uniqueVisitsPath += ` L ${xPos} ${uniqueVisitsY}`;
          // Only label the first and last dates
          if (index === 0 || index === numEntries - 1) {
            svgString += `<text x="${xPos - 10}" y="170" fill="black" font-family="Verdana" font-size="8">${entry.date}</text>`;}});
        // Draw the paths
        svgString += `<path d="${visitsPath}" stroke="red" stroke-width="1" fill="none"/>`;
        svgString += `<path d="${uniqueVisitsPath}" stroke="blue" stroke-width="1" fill="none"/>`;
        // Add the legend
        svgString += `
          <text x="50" y="190" fill="black" font-family="Verdana" font-size="10">Legend:</text>
          <line x1="100" y1="190" x2="150" y2="190" stroke="red" stroke-width="1"/>
          <text x="160" y="190" fill="black" font-family="Verdana" font-size="10">Visits</text>
          <line x1="200" y1="190" x2="250" y2="190" stroke="blue" stroke-width="1"/>
          <text x="260" y="190" fill="black" font-family="Verdana" font-size="10">Unique Visits</text>`;
        // Draw the border around the graph area
        svgString += `<rect x="50" y="30" width="${(numEntries - 1) * 20}" height="120" fill="none" stroke="black" stroke-width="1"/>`;
        // Build strings of values
        const visitsValues = "Visits: " + visitsData.join(", ");
        const uniqueVisitsValues = "Unique Visits: " + uniqueVisitsData.join(", ");
        // Display the values at the bottom
        svgString += `<text x="50" y="220" fill="black" font-family="Verdana" font-size="6">${visitsValues}</text>`;
        svgString += `<text x="50" y="240" fill="black" font-family="Verdana" font-size="6">${uniqueVisitsValues}</text>`;
        // Close the SVG tag
        svgString += `</svg>`;
        // Insert the SVG into the page
        graphContainer.innerHTML = svgString;
    });}
  graph();
  