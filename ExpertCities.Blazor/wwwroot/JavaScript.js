/* Toggle between adding and removing the "responsive" class to topnav when the user clicks on the icon */

function AnyFileSaveAs(bytesBase64, mimeType, fileName) {
    var fileUrl = "data:" + mimeType + ";base64," + bytesBase64;
    fetch(fileUrl)
        .then(response => response.blob())
        .then(blob => {
            var link = window.document.createElement("a");
            link.href = window.URL.createObjectURL(blob, { type: mimeType });
            link.download = fileName;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        });
}

function ScrollToSmooth(PageID) {
    document.getElementById(PageID).scrollIntoView({ behavior: 'smooth' })
}

function Throttle(ID, Time) {
    if (Time > 0) {
        document.getElementById(ID).disabled = true;
        setTimeout(function () {
            document.getElementById(ID).disabled = false;
        }, Time * 1000);
    }
}
var map;
function SignInMapBox(Token, Center, Zoom) {
    mapboxgl.accessToken = Token;
    map = new mapboxgl.Map({
        container: 'map',
        center: Center,
        style: 'mapbox://styles/mapbox/streets-v11',
        zoom: Zoom
    });
}
function AddLocation(NewUrl) {
    map.on('load', () => {
        map.addSource('places', {
            'type': 'geojson', 'data': {
                'type': 'FeatureCollection',
                'features': [
                    {
                        'type': 'Feature',
                        'properties': {
                            'title': 'Building 1',
                            'description':
                                `<strong>Building 1</strong><p><a href="${NewUrl}Assets/Building/1" target="_blank" title="Opens in a new window">Building 1</a>This could be a short description of the building</p>`,
                            'icon': 'school-15'
                        },
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [-74.61084084623728, 45.60228250701617, ]
                        }
                    },
                    {
                        'type': 'Feature',
                        'properties': {
                            'title': 'Building 2',
                            'description':
                                `<strong>Building 2</strong><p><a href="${NewUrl}Assets/Building/2" target="_blank" title="Opens in a new window">Building 2</a>This could be a short description of the building</p>`,
                            'icon': 'town-hall-15'
                        },
                        'geometry': {
                            'type': 'Point',
                            'coordinates': [-74.60503901404996, 45.60859520641057,]
                        }
                    }]
            }
        });
        map.addLayer({
            'id': 'places',
            'type': 'symbol',
            'source': 'places',
            'layout': {
                'icon-image': '{icon}',
                'icon-size': 1,
                'icon-allow-overlap': true,
                'text-field': ['get', 'title'],
                'text-font': [
                    'Open Sans Semibold',
                    'Arial Unicode MS Bold'
                ],
                'text-offset': [0, 1.25],
                'text-anchor': 'top',
                'text-size': 10

            },
        });
        // When a click event occurs on a feature in the places layer, open a popup at the
        // location of the feature, with description HTML from its properties.
        map.on('click', 'places', (e) => {
            // Copy coordinates array.
            const coordinates = e.features[0].geometry.coordinates.slice();
            const description = e.features[0].properties.description;

            // Ensure that if the map is zoomed out such that multiple
            // copies of the feature are visible, the popup appears
            // over the copy being pointed to.
            while (Math.abs(e.lngLat.lng - coordinates[0]) > 180) {
                coordinates[0] += e.lngLat.lng > coordinates[0] ? 360 : -360;
            }

            new mapboxgl.Popup()
                .setLngLat(coordinates)
                .setHTML(description)
                .addTo(map);
        });
        // Change the cursor to a pointer when the mouse is over the places layer.
        map.on('mouseenter', 'places', () => {
            map.getCanvas().style.cursor = 'pointer';
        });

        // Change it back to a pointer when it leaves.
        map.on('mouseleave', 'places', () => {
            map.getCanvas().style.cursor = '';
        });
    });
}