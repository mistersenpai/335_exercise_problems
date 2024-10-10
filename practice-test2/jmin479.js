function generateGraph() {
    const graphContainer = document.getElementById("graph");

    fetch("https://cws.auckland.ac.nz/Qz2021JGC/api/Version").then((response) => response.text()).then((data) => {
        //alert(data);

        

        const data1 = data.split('"')

        graphContainer.innerHTML += data1[1]
    })

}

generateGraph()