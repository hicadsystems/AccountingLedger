<template>
    <!-- WRAPPER -->
<div>

    <div v-if="errors" class="alert alert-danger alert-dismissible" role="alert">
        <div class="alert-message">
            {{ [errors] }}
        </div>
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">�</span>
        </button>
    </div>
    <div v-if="responseMessage" class="alert alert-primary alert-dismissible" role="alert"> <div class="alert-message"> {{ responseMessage }} </div> </div>
    <div class="card-body">
        <div class="form-group">
        <label class="form-label">Registration Number</label>
        <div class="row">
            <div class="col-12 col-xl-4">                    
                    <vuejsAutocomplete source="/api/StudentRecord/getAllStudentByNameLimited/"
                                       input-class="form-control"
                                       @selected="setValueStudent"
                                       v-model="pp">
                    </vuejsAutocomplete>

                </div>
            <div class="col-12 col-xl-2">
                <button class="btn btn-submit btn-primary" v-on:click="printClaim(studentid)" type="submit">Export to Pdf</button>
            </div>
        </div>

        <div></div>
        <div  v-if="StudentName">
        <div class="row">  <div class="col-12 col-xl-4"><strong> CLAIM HISTORY FOR {{StudentName.toUpperCase() }} </strong> </div>
        <div class="col-12 col-xl-4"><strong>REGISTRATION NUMBER: {{Reg_Number.toUpperCase() }} </strong> </div>
    </div>
        <table id="datatables-buttons" class="table table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>School Name</th>
                    <th>Class</th>
                    <th>Session</th>
                    <th>Term</th>
                    <th>Claim Amount</th>
                    <th>Total Claim Todate</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="claimsm in cliamSummaryList">
                    <td>{{ claimsm.schoolName }}</td>
                    <td>{{ claimsm.className }}</td>
                    <td>{{ claimsm.session }}</td>
                    <td>{{ claimsm.term }}</td>
                    <td>{{ claimsm.claimAmount }}</td>
                    <td>{{ claimsm.amountToDate }}</td>

                 </tr>
            </tbody>

        </table>

    </div>
</div>
</div>
  
</div>
                        
<!-- END WRAPPER -->
</template>

<script>
import Axios from 'axios';
import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
 components: {
     vuejsAutocomplete,
     Axios
    },
data() { 
    return {
    responseMessage:'',
        errors: null,
        searchData: '',
        submitorUpdate: 'Submit',
        cliamSummaryList: null,
        refno: false,
        regno: '',
        session : '',
        term : '',
        studentid:0,
        StudentName:'',
        Reg_Number:'',
    canProcess : true,
           
    };
    },
    mounted() {
            this.$store.state.objectToUpdate = null
        },
 
    watch:{
        '$store.state.objectToUpdate':function (newVal, oldval) {   }     
    },
    methods: {
        setValueStudent: function(result) {
        Axios
       .get(`/api/StudentClaim/getclaimsummary/${result.value}`)
       .then(response => {this.cliamSummaryList = response.data;
        this.studentid=response.data[0].studentid
        this.StudentName=response.data[0].studentName
        this.Reg_Number=response.data[0].reg_Number
       })
    },
    printClaim:function(studentid){
            window.open(`/SRClaimRecord/CLaimSummaryByPdf/${studentid}`)
        },
        viewDetails(result) {
            this.refno = true
            this.regno = result.Reg_Number
            this.session = result.session
            this.term = result.term

     }
    }
};
</script>