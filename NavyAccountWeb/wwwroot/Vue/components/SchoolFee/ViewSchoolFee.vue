<template>
    <div>
        
        <div class="card-body">
            <div class="row">
                <div class="col-12 col-xl-2">
                <button type="button" class="btn btn-submit btn-primary" @click="printSchoolFeeToPDF()">Export to PDF</button>
            </div>
            <div class="col-12 col-xl-2">
                <button type="button" class="btn btn-submit btn-primary" @click="printSchoolFeeToExcel()">Export to Excel</button>
            </div>
        </div>
                <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <td>School Session</td>
                        <td>School Type</td>
                        <td>Parent Status</td>
                        <td>Category</td>
                        <td>Amount</td>
                        
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="sch in schoolFeeList">
                        <td>{{sch.period}}</td>
                        <td>{{sch.type}}</td>
                        <td>{{sch.parentStatus}}</td>
                        <td>{{sch.classCategory}}</td>
                        <td>{{sch.amount}}</td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processRetrieve(sch)">Edit</button></td>
                        <td><button type="button" class="btn btn-submit btn-primary" @click="processDelete(sch.id)">Delete</button></td>

                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>


<script>
    
    export default { 
    data() {
        
        return {
        schoolFeeList:null
        };
    },
    created(){
        this.$store.state.objectToUpdate=null;
    },
    watch:{'$store.state.objectToUpdate':function(newval,oldval){
       this.getAllSchoolFee();
    }},
    mounted(){
        this.getAllSchoolFee();
    },
    methods:{
        processRetrieve:function(schoolfee){
        this.$store.state.objectToUpdate=schoolfee; 
      },
      getAllSchoolFee:function(){
        axios.get('/api/SchoolFee/GetAll')
        .then(respose=>
            (
                this.schoolFeeList=respose.data
            ));
      },
      processDelete:function(id){
        alert(id)
        axios.get(`/api/SchoolFee/DeleteRecord/${id}`)
        .then(response=>{
            if(response.data.responseCode='200'){
                alert("SchoolFee Successfully Deleted");
                this.getAllSchoolFee();
            }
        }).catch(e=>{
            this.error.push(e);
        });
      },
      printSchoolFeeToPDF:function(){
                window.open('/SRSchoolFee/SchoolFeeByPdf');
            },
            printSchoolFeeToExcel:function(){
                window.open('/SRSchoolFee/SchoolFeeByExcel');
            }
   }
}

</script>
