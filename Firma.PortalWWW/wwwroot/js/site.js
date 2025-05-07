// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
// Initialize active navigation
document.addEventListener('DOMContentLoaded', function () {
    const currentPath = window.location.pathname;
    document.querySelectorAll('.nav-link').forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.classList.add('active');
        }
    });
});

if (document.getElementById('bookingForm')) {
    flatpickr("#dateSelect", {
        minDate: "today",
        dateFormat: "Y-m-d",
        disable: [
            function (date) {
                return date.getDay() === 0; 
            }
        ],
        locale: {
            firstDayOfWeek: 1
        }
    });

    document.querySelectorAll('.time-slot').forEach(slot => {
        slot.addEventListener('click', () => {
            document.querySelectorAll('.time-slot').forEach(s => s.classList.remove('selected'));
            slot.classList.add('selected');
        });
    });

    document.getElementById('bookingForm').addEventListener('submit', (e) => {
        e.preventDefault();
        alert('Dziękujemy za rezerwację! Potwierdzenie zostanie wysłane na Twój adres email.');
    });
}