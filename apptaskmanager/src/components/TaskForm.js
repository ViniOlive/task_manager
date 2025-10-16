import React, { useEffect, useState } from "react";
import { createTask, getTask, updateTask, getTasks } from "../api/taskApi";
import { useNavigate, useParams } from "react-router-dom";

function TaskForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [titulo, setTitulo] = useState("");
  const [descricao, setDescricao] = useState("");
  const [dataConclusao, setDataConclusao] = useState("");
  const [status, setStatus] = useState("Pendente");
  const [error, setError] = useState("");

  useEffect(() => {
    if (id) {
      getTask(id)
        .then((res) => {
          const t = res.data;
          setTitulo(t.titulo);
          setDescricao(t.descricao || "");
          setStatus(t.status);
          setDataConclusao(
            t.dataConclusao
              ? new Date(t.dataConclusao).toISOString().slice(0, 10)
              : ""
          );
        })
        .catch(() => alert("Erro ao buscar a tarefa"));
    }
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (!titulo.trim()) {
      setError("O título é obrigatório.");
      return;
    }
    if (titulo.length > 100) {
      setError("O título deve ter no máximo 100 caracteres.");
      return;
    }

    try {
      const allTasks = await getTasks();
      const duplicated = allTasks.data.some(
        (t) =>
          t.titulo.trim().toLowerCase() === titulo.trim().toLowerCase() &&
          (!id || t.id !== parseInt(id))
      );

      if (duplicated) {
        setError("Já existe uma tarefa com este título.");
        return;
      }
    } catch (err) {
      console.error("Erro ao validar duplicidade:", err);
    }

    const now = new Date().toISOString().split("T")[0];
    const creationDate = id ? null : now;
    if (dataConclusao) {
      const conclusao = new Date(dataConclusao).toISOString().split("T")[0];
      if (conclusao < creationDate) {
        setError("A data de conclusão não pode ser anterior à data de criação.");
        return;
      }
    }

    const payload = {
      titulo: titulo.trim(),
      descricao: descricao.trim(),
      dataConclusao: dataConclusao
        ? new Date(dataConclusao).toISOString()
        : null,
      status,
    };
    try {
      if (id) {
        await updateTask(id, payload);
      } else {
        await createTask(payload);
      }
      navigate("/");
    } catch (err) {
      console.error(err);
      if (err.response?.status === 409)
        setError("Já existe uma tarefa com esse título.");
      else if (err.response?.data)
        setError(err.response.data);
      else setError("Erro ao salvar tarefa. Tente novamente.");
    }
  };

  return (
    <div style={{ padding: 16 }}>
      <h2>{id ? "Editar" : "Nova"} tarefa</h2>
      <form onSubmit={handleSubmit}>
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
          <label>Título</label>
          <br />
          <input
            type="text"
            value={titulo}
            onChange={(e) => setTitulo(e.target.value)}
            maxLength={100}
            required
          />
        </div>

        <div>
          <label>Descrição</label>
          <br />
          <textarea
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
          />
        </div>

        <div>
          <label>Data conclusão</label>
          <br />
          <input
            type="date"
            value={dataConclusao}
            onChange={(e) => setDataConclusao(e.target.value)}
          />
        </div>

        {id && (
          <div>
            <label>Status</label>
            <br />
            <select value={status} onChange={(e) => setStatus(e.target.value)}>
              <option value="Pendente">Pendente</option>
              <option value="EmProgresso">EmProgresso</option>
              <option value="Concluida">Concluida</option>
            </select>
          </div>
        )}

        <div style={{ marginTop: 8 }}>
          <button type="submit">Salvar</button>
        </div>
      </form>
    </div>
  );
}

export default TaskForm;
