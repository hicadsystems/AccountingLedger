<template>
    <div>
        
        <div class="card-body">
              <div class="row">
                        <div class="col-12 col-xl-2">
                            <button class="btn btn-submit btn-primary" v-on:click="printSchoolReportAsPdf" type="submit">Export to Pdf</button>
                        </div>
                        <div class="col-12 col-xl-2">
                            <button class="btn btn-submit btn-success" v-on:click="printSchoolReportAsExcel" type="submit">Export to Excel</button>
                        </div>
                    </div>
            <table id="datatables-buttons" class="table table-striped" style="width:100%">
                <thead>
                    <tr>
                        <td>School Code</td>
                        <td>School Name</td>
                        <td>School Address</td>
                        <td>School City</td>
                        <td>School State</td>
                        
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="sch in schoolList">
                        <td>{{sch.schoolCode}}</td>
                        <td>{{sch.schoolname}}</td>
                        <td>{{sch.schoolAddress}}</td>
                        <td>{{sch.schoolCity}}</td>
                        <td>{{sch.schoolState}}</td>
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
        schoolList:null
        };
    },
    created(){
        this.$store.state.objectToUpdate=null;
    },
    watch:{'$store.state.objectToUpdate':function(newval,oldval){
       this.getAllSchool();
    }},
    mounted(){
        this.getAllSchool();
    },
    methods:{
        processRetrieve:function(school){
        this.$store.state.objectToUpdate=school; 
        },
        printSchoolReportAsPdf:function(){
         window.open('/SRPaymentRecord/PrintFilterSchoolWithStudentPdf')
        },
     printSchoolReportAsExcel:function(){
     window.open('/SRPaymentRecord/PrintFilterSchoolWithStudentAsExcel')

},

      getAllSchool:function(){
        axios.get('/api/SchoolRecord/GetAll')
        .then(respose=>
            (
                this.schoolList=respose.data
            ));
      },
      processDelete:function(id){
        alert(id)
        axios.get(`/api/SchoolRecord/DeleteRecord/${id}`)
        .then(response=>{
            if(response.data.responseCode='200'){
                alert("School Successfully Deleted");
                this.getAllSchool();
            }
        }).catch(e=>{
            this.error.push(e);
        });
      }
   }
}

</script>
