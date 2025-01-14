const searchBar = document.querySelector('.search-bar');
searchBar.addEventListener('keydown', (event) => {
    highlightTask();
});
function highlightTask() {
    const tasks = document.querySelectorAll('.task-card')
    tasks.forEach(taskCard => {
        const taskTitle = taskCard.querySelector('.task-title');
        console.log(taskTitle.textContent);

        if (taskTitle.textContent.toLowerCase().includes(searchBar.value.toLowerCase()) && searchBar.value != '' && searchBar.value != null) {
            taskCard.classList.add('highlighted');
        }
        else {
            taskCard.classList.remove('highlighted');
        }
    });
}