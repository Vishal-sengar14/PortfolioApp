document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('sidebarToggle')?.addEventListener('click', () => {
        document.querySelector('.admin-sidebar')?.classList.toggle('open');
    });

    document.querySelectorAll('.js-confirm-delete').forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault();
            if (window.Swal) {
                Swal.fire({
                    title: 'Are you sure?',
                    text: 'This action cannot be undone.',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#f87171',
                    cancelButtonColor: '#4f7cff',
                    confirmButtonText: 'Yes, delete it',
                    background: '#101425',
                    color: '#e6e9f5'
                }).then((result) => {
                    if (result.isConfirmed) form.submit();
                });
            } else if (confirm('Are you sure you want to delete this item?')) {
                form.submit();
            }
        });
    });
});
