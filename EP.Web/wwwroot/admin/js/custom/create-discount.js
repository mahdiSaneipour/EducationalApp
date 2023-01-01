var options = {
    calendarType: 'persian', // DONE
    format: 'YYYY/MM/DD',
    calendar: {
        persian: {
            locale: 'en', // DONE
            showHint: true, // DONE
            leapYearMode: 'algorithmic' // "astronomical" // DONE
        },
    }
}

$(".date-picker").pDatepicker(options);