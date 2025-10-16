import React, { useEffect, useState } from "react";
import { getTasks, deleteTask } from "../api/taskApi";
import { Link } from "react-router-dom";

function TaskList() {
  const [tasks, setTasks] = useState([]);
  const [filter, setFilter] = useState("All");
  const [error, setError] = useState("");

  const fetch = async () => {
    try {
      const res = await getTasks();
      setTasks(res.data);
    } catch (err) {
      console.error(err);
      setError("Erro ao carregar tarefas. Verifique a API.");
    }
  };

  useEffect(() => { fetch(); }, []);

  const handleDelete = async (id) => {
    if (!window.confirm("Confirma exclusão da tarefa?")) return;
    try {
      await deleteTask(id);
      fetch();
    } catch (err) {
      console.error(err);
      alert("Erro ao excluir tarefa.");
    }
  };

  const filtered = tasks.filter((t) => {
    if (filter === "All") return true;
    return t.status === filter;
  });

  return (
    <div style={{ padding: 16 }}>
      <h2>Lista de Tarefas</h2>

      {error && (
        <div
          style={{
            backgroundColor: "#ffe5e5",
            color: "#b30000",
            padding: 8,
            borderRadius: 4,
            marginBottom: 8,
          }}
        >
          {error}
        </div>
      )}

      <div>
        <label>Filtrar: </label>
        <select value={filter} onChange={(e) => setFilter(e.target.value)}>
          <option value="All">Todas</option>
          <option value="Pendente">Pendente</option>
          <option value="EmProgresso">EmProgresso</option>
          <option value="Concluida">Concluida</option>
        </select>
      </div>

      <table style={{ marginTop: 12 }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Status</th>
            <th>Data criação</th>
            <th>Data Conclusão</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {filtered.map((t) => (
            <tr key={t.id}>
              <td>{t.id}</td>
              <td>{t.titulo}</td>
              <td>{t.status}</td>
              <td>{new Date(t.dataCriacao).toLocaleString()}</td>
              <td>{t.dataConclusao ? new Date(t.dataConclusao).toLocaleString().slice(0, 10) : "Tarefa não concluída"}</td>
              <td>
                <Link to={`/edit/${t.id}`} className="link-edit">
                  Editar
                </Link>
                {" | "}
                <button
                  onClick={() => handleDelete(t.id)}
                  className="delete-btn"
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default TaskList;
