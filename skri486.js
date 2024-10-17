function graph() {
  const SC = document.getElementById("graph");
  fetch("https://cws.auckland.ac.nz/nzsl/api/Log")
    .then((res) => res.json())
    .then((data) => {
      const totalCount = data.length;
      const maxVisits = Math.max(...data.map(d => d.visits));
      const maxUniqueVisits = Math.max(...data.map(d => d.uniqueVisits));
      const maxValue = Math.max(maxVisits, maxUniqueVisits);

      const minVisits = Math.min(...data.map(d => d.visits));
      const minUniqueVisits = Math.min(...data.map(d => d.uniqueVisits));
      const minValue = Math.min(minVisits, minUniqueVisits);

      let svgString = `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 700 300">
      
      <rect width="${(data.length*20)+100}" height="300" fill="white" />
      <text x="10" y="20" fill="black" font-family="verdana" font-size="12" font-weight="900">Visit Log</text>`;

      let VistsVals = "Visits: ";
      let UniqueVals = "Unique Visits: ";
      let count = 0;

      data.forEach((item) => {
        if (count != totalCount-1 ) {
          VistsVals += `${item.visits}, `;
          UniqueVals += `${item.uniqueVisits}, `;
        } else {
          VistsVals += `${item.visits} `;
          UniqueVals += `${item.uniqueVisits} `;
        }
        
        count += 1;
      });

      svgString += `
                    
                    <text x="30" y="30" fill="black" font-family="verdana" font-size="8">${maxValue}</text>
                    <text x="35" y="150" fill="black" font-family="verdana" font-size="8">${minValue}</text>`;

      let visitsPath = `M 50 150 `;
      let uniqueVisitsPath = `M 50 150 `;

      data.forEach((item, index) => {
        let x = 50 + index * 20; 
        let visitsYpt = 150 - ((item.visits - minValue) / (maxValue - minValue)) * 120;
        let uniqueVisitsYpt = 150 - ((item.uniqueVisits - minValue) / (maxValue - minValue)) * 120;
        
        visitsPath += `L ${x} ${visitsYpt} `;
        uniqueVisitsPath += `L ${x} ${uniqueVisitsYpt} `;

        if (index === 0 || index === totalCount - 1) {
          svgString += `<text x="${x - 10}" y="170" fill="black" font-family="verdana" font-size="8" transform="rotate(0,${x},170)">${item.date}</text>`;
        }
      });
      svgString += `<path d="${visitsPath}" stroke="red" stroke-width="1" fill="none"/>`;

      svgString += `<path d="${uniqueVisitsPath}" stroke="blue" stroke-width="1" fill="none"/>`;

      svgString += `<text x="50" y="190" fill="black" font-family="verdana" font-size="10">Legend:</text>
                    <line x1="100" y1="190" x2="150" y2="190" stroke="red" stroke-width="1"/>
                    <text x="160" y="190" fill="black" font-family="verdana" font-size="10">Visits</text>
                    <line x1="200" y1="190" x2="250" y2="190" stroke="blue" stroke-width="1"/>
                    <text x="260" y="190" fill="black" font-family="verdana" font-size="10">Unique visits</text>
                    <rect x="50" y="30" width="${(totalCount-1)*20}" height="120" fill="none" stroke="black" stroke-width="1"/>
                    <text x="150" y="205" fill="black" font-family="verdana" font-size="3">${VistsVals}</text>
                    <text x="150" y="210" fill="black" font-family="verdana" font-size="3">${UniqueVals}</text>
                    `
                    ;

      svgString += `</svg>`;

      SC.innerHTML = svgString;
    });
}

graph();
