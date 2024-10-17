function generateGraph() {
    const graphContainer = document.getElementById("graph");

    fetch("https://cws.auckland.ac.nz/Qz2021JGC/api/CaseCounts")
    .then((response) => response.text())
    .then((data) => {

        const len = data.length;

        const toJson = data.toJson;

        Object.entries(data).forEach(([key, value]) => 
            {
                console.log(`key: ${key} value: ${value}`)
    
            });
        console.log(toJson)

        // for (let i = 0; i < 5; i++) {
        //     console.log(data[i]);
        //     graphContainer.innerHTML += data[i];
        //   }

        

        // const data1 = data.split('"')

        graphContainer.innerHTML += data
    })

}

generateGraph()