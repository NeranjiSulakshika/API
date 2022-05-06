import React, { Component } from "react";

// Import materials
import Button from "@material-ui/core/Button";
import DeleteIcon from "@material-ui/icons/Delete";

// Import scss
import "./AdminHomePage.scss";

// Import Services
import AuthServices from "../services/AuthServices";

const service = new AuthServices();

export default class AdminHomePage extends Component {
  constructor() {
    super();
    this.state = {
      userId: "",
      nic: "",
      name: "",
      address: "",
      age: "",
      profession: "",
      email: "",
      qualification: "",
      DataRecord: [],
    };
  }

  componentWillMount() {
    console.log("Component will mount calling");
    this.GetCitizenList();
  }

  // Get Citizen List
  GetCitizenList() {
    service
      .GetCitizenList()
      .then((data) => {
        console.log(data);
        console.log(data.data.readCitizenInformation);
        this.setState({ DataRecord: data.data.readCitizenInformation });
      })
      .catch((error) => {
        console.log(error);
      });
  }

  // Handle Delete
  handleDelete = (datas) => {
    const data = {
      id: Number(datas.userId),
    };

    service
      .DeleteCitizen(data)
      .then((data) => {
        console.log(data);
        this.GetCitizenList();
      })
      .catch((error) => {
        console.log(error);
      });
  };

  render() {
    let state = this.state;
    let Self = this;
    return (
      <div className="MainContainer">
        <div className="SubContainer">
          <div className="Box1">
            {Array.isArray(this.state.DataRecord) &&
            this.state.DataRecord.length > 0 ? (
              this.state.DataRecord.map(function (data, index) {
                return (
                  <div key={index} className="data-flex">
                    <div className="UserId">{data.userId}</div>
                    <div className="NIC">{data.nic}</div>
                    <div className="Name">{data.name}</div>
                    <div className="Address">{data.address}</div>
                    <div className="Age">{data.age}</div>
                    <div className="Profession">{data.profession}</div>
                    <div className="Email">{data.email}</div>
                    <div className="Qualification">{data.qualification}</div>
                    <div className="DeleteCitizen">
                      <Button
                        variant="outlined"
                        onClick={() => {
                          Self.handleDelete(data);
                        }}
                      >
                        <DeleteIcon />
                      </Button>
                    </div>
                  </div>
                );
              })
            ) : (
              <div></div>
            )}
          </div>
        </div>
      </div>
    );
  }
}
