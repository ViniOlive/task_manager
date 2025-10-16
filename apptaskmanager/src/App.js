import {Routes, Route, Link } from "react-router-dom";
import TaskList from "./components/TaskList";
import TaskForm from "./components/TaskForm";
import "./css/App.css";

function App() {
  return (
    <div className="app-container">
      <nav>
        <Link to="/">Tarefas</Link> | <Link to="/new">Nova tarefa</Link>
      </nav>
      <Routes>
        <Route path="/" element={<TaskList />} />
        <Route path="/new" element={<TaskForm />} />
        <Route path="/edit/:id" element={<TaskForm />} />
      </Routes>
    </div> 
  );
}

export default App;
