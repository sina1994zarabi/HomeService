// site.js

// Wait for the DOM to fully load before running scripts
$(document).ready(function () {
    // Handle a button click (assuming the button has id="myButton")
    $('#myButton').on('click', function () {
        alert('Button clicked!');
    });

    // Function to fetch data from the server using AJAX
    function fetchData() {
        $.ajax({
            url: '/Home/GetData', // URL to a controller action (e.g., HomeController.GetData)
            type: 'GET',          // HTTP method
            success: function (data) {
                console.log('Data received:', data);
                // Update a div with id="dataContainer" with the received data
                $('#dataContainer').html(data);
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }

    // Uncomment to run the function on page load
    // fetchData();
});