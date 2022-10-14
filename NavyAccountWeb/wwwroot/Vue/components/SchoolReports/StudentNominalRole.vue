<template>
    <!-- WRAPPER -->
<div>
    <div class="card-body">
    <div class="form-group">
     <div class="row">
        <div class="col-sm-4">
        <label class="form-label">School Name</label>
            <select class="form-control" v-model="postBody.schoolId" name="schoolname">
                <option v-bind:value="0" v-bind:key="0"> All </option>
                <option v-for="sch in schoolList" v-bind:value="sch.id" v-bind:key="sch.id"> {{ sch.schoolname }} </option>
            </select>
        </div>
        <div class="col-md-4">
        <label class="form-label">Class</label>
            <select class="form-control" v-model="postBody.classId" name="schoolname" >
                <option v-bind:value="0" v-bind:key="0"> All </option>
                <option v-for="cls in classList" v-bind:value="cls.id" v-bind:key="cls.id"> {{ cls.className }} </option>
            </select>
        </div>
        
        <div class="form-group col-md-4">
           <label>Status</label>
         <select class="form-control" v-model="postBody.Status" name="status" required>
            <option v-bind:value="0" v-bind:key="0"> All </option>
            <option v-for="stu in statusList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
          </select>
        </div>
        <div class="col-md-4">
          <label>Parential Status</label>
            <select class="form-control" v-model="postBody.ParentalStatus" name="status">
                <option v-bind:value="0" v-bind:key="0"> All </option>
                 <option v-for="stu in parentalStatusList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
            </select>
        </div>
        <!-- <div class="col-md-4">
            <label>Enrolment Date</label>
                <vuejsDatepicker v-model="postBody.CommencementDate" input-class="form-control" name="commencementDate"></vuejsDatepicker>
        </div> -->
        <div class="col-md-4">
          <label>Sort By</label>
            <select class="form-control" v-model="postBody.sortby" name="status">
                
                 <option v-for="stu in exitreasonList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
            </select>
        </div>
        
    </div>
    <div class="row">
        <div class="col-12 "> 
            <div class="btn-group mr-2 sw-btn-group-extra" role="group">
                <button class="btn btn-submit btn-primary" 
                v-on:click="showStudnetList(postBody.schoolId,postBody.classId,postBody.Status,postBody.ParentalStatus,postBody.sortby)" 
                 type="submit">Show</button>
            </div>
        </div>
        <div class="col-12 col-xl-2" style="margin-left:600px;" >
            <button class="btn btn-submit btn-primary" 
            v-on:click="printAsPdf(postBody.schoolId,postBody.classId,postBody.Status,postBody.ParentalStatus,postBody.sortby)" 
            type="submit">Export to Pdf</button>
        </div>
        <div class="col-12 col-xl-2">
            <button class="btn btn-submit btn-success"
             v-on:click="printAsExcel(postBody.schoolId,postBody.classId,postBody.Status,postBody.ParentalStatus,postBody.sortby)"
              type="submit">Export to Excel</button>
        </div>

    </div>
        <table id="datatables-buttons" class="table table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>Registration Number</th>
                    <th>Full Name</th>
                    <th>Parent Name </th>
                    <th>Gender</th>
                    <th>Age</th>
                    <th>School Name</th>
                    <th>School Type</th>
                    <th>Class</th>
                    
                </tr>
            </thead>
            <tbody>
                <tr v-for="student in studentList">
                    <td>{{ student.reg_Number }}</td>
                    <td>{{ student.studentName}}</td>
                    <td>{{ student.parentName }}</td>
                    <td>{{ getAppropriateGender(student.sex) }}</td>
                    <td>{{ student.age }}</td>
                    <td>{{ student.schoolName }}</td>
                    <td>{{ student.schoolCode }}</td>
                    <td>{{ student.className }}</td>
                    
                  </tr>
            </tbody>

        </table>
        <paginate
           
            :page-count="getPageCount"
            :page-range="3"
            :margin-pages="2"
            :click-handler="clickCallback"
            :prev-text="'Prev'"
            :next-text="'Next'"
            class="pagination"
            :container-class="'pagination'"
            :page-class="'page-item'">
        </paginate>
    </div>
</div>

</div>

    <!-- END WRAPPER -->
</template>

<script>
import Paginate from 'vuejs-paginate'
import vuejsAutocomplete from 'vuejs-auto-complete'
import vuejsDatepicker from 'vuejs-datepicker'
export default {
components:{
    Paginate,
     vuejsAutocomplete,
     vuejsDatepicker
},
data() {
    return {
    studentList:null,
    schoolList:null,
    classList:null,
    statusList:null,
    exitreasonList:null,
    parentalStatusList:null,
    pageno:0,
    totalcount:0,
      id:0,
     pp:'',
     postBody:{
                Status:'0',
                CommencementDate:null,
                ParentalStatus:'0',
                ClassId:0,
                SchoolId:0,
                sortby:'0'
            },
            parentalStatusList: [
                    { value: 'Military', text: 'Military' },
                    { value: 'Civilian', text: 'Civilian' }
                ],
                statusList: [
                    { value: 'Active', text: 'Active' },
                    { value: 'OnClaim', text: 'On Claim' },
                    { value: 'Inactive', text: 'Inactive' }
                ],
                exitreasonList: [
                    { value: 'School', text: 'School' },
                    { value: 'Class', text: 'Class' },
                    { value: 'ParentialStatus', text: 'ParentialStatus' },
                    { value: 'Status', text: 'Status' }

                ]

    };
},
computed: {
    getPageCount :function() {
        return ( Math.ceil(this.totalcount/10) - 0);
    }
},
mounted () {
        axios
        .get('/api/SchoolRecord/GetAll')
        .then(response => (this.schoolList = response.data));
        axios
        .get('/api/statictable/getallclass')
        .then(response => (this.classList = response.data));
    axios
    .get(`/api/StudentRecord/getAllStudent2s?pageno=${this.pageno}`)
    .then(response => {
        this.studentList =  response.data.studentlist;
        this.totalcount = response.data.total;
    })
 },
 methods: {
        showStudnetList:function(schoolId,classId,Status,ParentalStatus,sortby){
            axios
            .get(`/api/StudentRecord/getStudentNorminalRoll/${schoolId}/${classId}/${Status}/${ParentalStatus}/${sortby}`)
            .then(response => {this.studentList = response.data;
            })
        },
        printAsPdf:function(schoolId,classId,Status,ParentalStatus,sortby){
            window.open(`/SRSchoolReport/PrintStudentByPdf/${schoolId}/${classId}/${Status}/${ParentalStatus}/${sortby}`)
        },
        printAsExcel:function(schoolId,classId,Status,ParentalStatus,sortby){
           window.open(`/SRSchoolReport/PrintStudentByExcel/${schoolId}/${classId}/${Status}/${ParentalStatus}/${sortby}`)
        },
     clickCallback: function (pageNum) {
         this.pageno = pageNum;
         axios.get(`/api/StudentRecord/getAllStudents?pageno=${this.pageno}`)
        .then(response => {
            
            this.studentList = response.data.studentlist;
            this.totalcount = response.data.total;
        })
     },
      setValueStudent: function(result) {
          alert(result.value)
         axios
       .get(`/api/StudentRecord/getStudentByID/${result.value}`)
       .then(response => {this.studentList = response.data;
      
       })
    },

     processRetrieve: function (mainAccount) {

        //this.$store.state.objectToUpdate = mainAccount;
     },

     getAppropriateGender: function (gender) {
         if (gender == 'M')
             return 'Male';
         if (gender == 'F')
             return 'Female';
     }
}

};
</script>
<style lang="css">

.pagination{display:inline-block;padding-left:0;margin:20px 0;border-radius:4px}.pagination>li{display:inline}.pagination>li>a,.pagination>li>span{position:relative;float:left;padding:6px 12px;margin-left:-1px;line-height:1.42857143;color:#337ab7;text-decoration:none;background-color:#fff;border:1px solid #ddd}.pagination>li>a:focus,.pagination>li>a:hover,.pagination>li>span:focus,.pagination>li>span:hover{z-index:2;color:#23527c;background-color:#eee;border-color:#ddd}.pagination>li:first-child>a,.pagination>li:first-child>span{margin-left:0;border-top-left-radius:4px;border-bottom-left-radius:4px}.pagination>li:last-child>a,.pagination>li:last-child>span{border-top-right-radius:4px;border-bottom-right-radius:4px}.pagination>.active>a,.pagination>.active>a:focus,.pagination>.active>a:hover,.pagination>.active>span,.pagination>.active>span:focus,.pagination>.active>span:hover{z-index:3;color:#fff;cursor:default;background-color:#337ab7;border-color:#337ab7}.pagination>.disabled>a,.pagination>.disabled>a:focus,.pagination>.disabled>a:hover,.pagination>.disabled>span,.pagination>.disabled>span:focus,.pagination>.disabled>span:hover{color:#777;cursor:not-allowed;background-color:#fff;border-color:#ddd}.pagination-lg>li>a,.pagination-lg>li>span{padding:10px 16px;font-size:18px;line-height:1.3333333}.pagination-lg>li:first-child>a,.pagination-lg>li:first-child>span{border-top-left-radius:6px;border-bottom-left-radius:6px}.pagination-lg>li:last-child>a,.pagination-lg>li:last-child>span{border-top-right-radius:6px;border-bottom-right-radius:6px}.pagination-sm>li>a,.pagination-sm>li>span{padding:5px 10px;font-size:12px;line-height:1.5}.pagination-sm>li:first-child>a,.pagination-sm>li:first-child>span{border-top-left-radius:3px;border-bottom-left-radius:3px}.pagination-sm>li:last-child>a,.pagination-sm>li:last-child>span{border-top-right-radius:3px;border-bottom-right-radius:3px}.pager{padding-left:0;margin:20px 0;text-align:center;list-style:none}.pager li{display:inline}.pager li>a,.pager li>span{display:inline-block;padding:5px 14px;background-color:#fff;border:1px solid #ddd;border-radius:15px}.pager li>a:focus,.pager li>a:hover{text-decoration:none;background-color:#eee}.pager .next>a,.pager .next>span{float:right}.pager .previous>a,.pager .previous>span{float:left}.pager .disabled>a,.pager .disabled>a:focus,.pager .disabled>a:hover,.pager .disabled>span{color:#777;cursor:not-allowed;background-color:#fff}

</style> 