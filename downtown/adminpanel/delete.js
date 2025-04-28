document.querySelectorAll('.delete-btn').forEach(button => {
    button.addEventListener('click', async e => {
      const li      = e.target.closest('li');
      const imageId = li.dataset.id;
  
      if (!confirm('Are you sure you want to delete this image?')) return;
  
      try {
        const res = await fetch(`/api/images/${imageId}`, {
          method: 'DELETE',
          headers: { 'Accept': 'application/json' }
        });
        const body = await res.json();
  
        if (res.ok && body.success) {
          li.remove();
        } else {
          alert(body.error || 'Failed to delete');
        }
      } catch (err) {
        console.error(err);
        alert('Network error');
      }
    });
  });
  