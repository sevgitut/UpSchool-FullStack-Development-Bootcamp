import { useEffect, useState } from 'react';
import './App.css';
import { Button, Container, Form, Grid, Header, Icon, Input, Table, Segment, Checkbox } from 'semantic-ui-react';
import TodoDto from './types/TodoDto';

function App() {
  const [todos, setTodos] = useState<TodoDto[]>([]);
  const [newTodo, setNewTodo] = useState('');
  const [task, setTask] = useState<string>('');
  const [savedTask, setSavedTask] = useState<TodoDto[]>([]);
  const [sortByDate, setSortByDate] = useState(false);

  const handleAddTodo = () => {
    if (newTodo.trim() === '') return;

    const todo: TodoDto = {
      id: new Date().getTime(),
      task: newTodo,
      isCompleted: false,
      createdDate: new Date(),
    };

    setTodos((prevTodos) => [...prevTodos, todo]);
    setNewTodo('');
  };

  const handleToggleComplete = (id: number) => {
    setTodos((prevTodos) =>
      prevTodos.map((todo) =>
        todo.id === id ? { ...todo, isCompleted: !todo.isCompleted } : todo
      )
    );
  };

  const handleDeleteTodo = (id: number) => {
    setTodos((prevTodos) => prevTodos.filter((todo) => todo.id !== id));
  };

  useEffect(() => {
    handleToDoItem();
  }, []);

  const handleToDoItem = (): void => {
    setTask('');
  };

  const handleSortByDate = () => {
    setSortByDate(!sortByDate);
  };

  const sortedTodos = [...todos].sort((a, b) => {
    if (sortByDate) {
      return a.createdDate.getTime() - b.createdDate.getTime();
    } else {
      return b.createdDate.getTime() - a.createdDate.getTime();
    }
  });

  return (
    <div className="app container">
      <h1>Todos</h1>
      <div className="ui action input">
        <input
          type="text"
          placeholder="Enter a new todo..."
          value={newTodo}
          onChange={(e) => setNewTodo(e.target.value)}
        />
        <Button primary onClick={handleAddTodo} disabled={newTodo.trim() === ''}>
          Add
        </Button>
      </div>
      <Segment>
        <div className="sort-button">
          <Button primary onClick={handleSortByDate}>
            {sortByDate ? 'Sort by Oldest' : 'Sort by Newest'}
          </Button>
        </div>
        <Table celled>
          <Table.Header>
            <Table.Row>
              <Table.HeaderCell>Task</Table.HeaderCell>
              <Table.HeaderCell>Created Date</Table.HeaderCell>
              <Table.HeaderCell>Completed</Table.HeaderCell>
              <Table.HeaderCell>Delete</Table.HeaderCell>
            </Table.Row>
          </Table.Header>
          <Table.Body>
            {sortedTodos.map((todo) => (
              <Table.Row key={todo.id}>
                <Table.Cell>{todo.task}</Table.Cell>
                <Table.Cell>{todo.createdDate.toLocaleString()}</Table.Cell>
                <Table.Cell>
                  <Checkbox
                    checked={todo.isCompleted}
                    onChange={() => handleToggleComplete(todo.id)}
                  />
                </Table.Cell>
                <Table.Cell>
                  <Button icon color="red" onClick={() => handleDeleteTodo(todo.id)}>
                    <Icon name="trash" />
                  </Button>
                </Table.Cell>
              </Table.Row>
            ))}
          </Table.Body>
        </Table>
      </Segment>
    </div>
  );
}

export default App;
